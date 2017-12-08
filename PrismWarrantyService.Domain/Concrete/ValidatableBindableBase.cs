using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PrismWarrantyService.Domain.Concrete
{
    public class ValidatableBindableBase : BindableBase, INotifyDataErrorInfo
    {
        #region Fields

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private readonly Dictionary<string, List<string>> _errors;
        private readonly object _locker;

        #endregion

        #region Constructors and finalizers

        public ValidatableBindableBase()
        {
            _errors = new Dictionary<string, List<string>>();
            _locker = new object();
            // Validate();
        }

        #endregion

        #region Properties

        public bool IsValid => !HasErrors;

        public bool HasErrors => _errors.Any(propertyErrors => propertyErrors.Value != null && propertyErrors.Value.Count > 0);

        #endregion

        #region Methods

        public IEnumerable GetErrors(string propertyName = null)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                if (_errors.ContainsKey(propertyName) && _errors[propertyName] != null && _errors[propertyName].Count > 0)
                    return _errors[propertyName].ToList();

                return null;
            }

            return _errors.SelectMany(err => err.Value.ToList());
        }

        public void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            lock (_locker)
            {
                var validationContext = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new List<ValidationResult>();

                Validator.TryValidateProperty(value, validationContext, validationResults);

                if (_errors.ContainsKey(propertyName))
                    _errors.Remove(propertyName);
                OnErrorsChanged(propertyName);

                HandleValidationResults(validationResults);
            }
        }

        public void Validate()
        {
            lock (_locker)
            {
                var validationContext = new ValidationContext(this, null, null);
                var validationResults = new List<ValidationResult>();

                Validator.TryValidateObject(this, validationContext, validationResults, true);

                var propertyNames = _errors.Keys.ToList();
                _errors.Clear();
                propertyNames.ForEach(OnErrorsChanged);

                HandleValidationResults(validationResults);
            }
        }

        private void HandleValidationResults(List<ValidationResult> validationResults)
        { 
            var resultsByPropertyNames = from result in validationResults
                                         from memberName in result.MemberNames
                                         group result by memberName into results
                                         select results;

            foreach (var property in resultsByPropertyNames)
            {
                var messages = property.Select(result => result.ErrorMessage).ToList();
                _errors.Add(property.Key, messages);
                OnErrorsChanged(property.Key);
            }
        }

        public void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion
    }
}
