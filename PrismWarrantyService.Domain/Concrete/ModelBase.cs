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
    public class ModelBase : BindableBase, INotifyDataErrorInfo
    {
        #region Fields

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private Dictionary<string, List<string>> errors;
        private object locker;

        #endregion

        #region Constructors and finalizers

        public ModelBase()
        {
            errors = new Dictionary<string, List<string>>();
            locker = new object();
            // Validate();
        }

        #endregion

        #region Properties

        public bool IsValid
        {
            get => !HasErrors;
        }

        public bool HasErrors
        {
            get => errors.Any(propertyErrors => propertyErrors.Value != null && propertyErrors.Value.Count > 0);
        }

        #endregion

        #region Methods

        public IEnumerable GetErrors(string propertyName = null)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                if (errors.ContainsKey(propertyName) && errors[propertyName] != null && errors[propertyName].Count > 0)
                    return errors[propertyName].ToList();

                return null;
            }

            return errors.SelectMany(err => err.Value.ToList());
        }

        public void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            lock (locker)
            {
                var validationContext = new ValidationContext(this, null, null) { MemberName = propertyName };
                var validationResults = new List<ValidationResult>();

                Validator.TryValidateProperty(value, validationContext, validationResults);

                if (errors.ContainsKey(propertyName))
                    errors.Remove(propertyName);
                OnErrorsChanged(propertyName);

                HandleValidationResults(validationResults);
            }
        }

        public void Validate()
        {
            lock (locker)
            {
                var validationContext = new ValidationContext(this, null, null);
                var validationResults = new List<ValidationResult>();

                Validator.TryValidateObject(this, validationContext, validationResults, true);

                var propertyNames = errors.Keys.ToList();
                errors.Clear();
                propertyNames.ForEach(pn => OnErrorsChanged(pn));

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
                errors.Add(property.Key, messages);
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
