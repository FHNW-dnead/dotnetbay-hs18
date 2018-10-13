using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBay.WPF.ViewModel
{
    /// <summary>
    /// Implements general NotifyPropertyChanged functionality which can 
    /// be used by ViewModel classes. 
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Extracts the name of a property from an expression.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the property.
        /// </typeparam>
        /// <param name="propertyExpression">
        /// An expression returning the property's name.
        /// </param>
        /// <returns>
        /// The name of the property returned by the expression.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If the expression is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If the expression does not represent a property.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "This design is better than the alternative.")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This design is better than the alternative.")]
        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var body = propertyExpression.Body as MemberExpression;
            if (body == null)
            {
                throw new ArgumentException("Invalid argument", "propertyExpression");
            }

            var property = body.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("Argument is not a property", "propertyExpression");
            }

            return property.Name;
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = this.PropertyChanged;
            handler?.Invoke(this, e);
        }

        protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
        {
            var handler = this.PropertyChanging;
            handler?.Invoke(this, e);
        }

        protected bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue) where T : class
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            this.RaisePropertyChanging(propertyExpression);

            field = newValue;

            this.RaisePropertyChanged(propertyExpression);
            return true;
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = GetPropertyName(propertyExpression);
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected void RaisePropertyChanging<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = GetPropertyName(propertyExpression);
            this.OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
        }
    }
}
