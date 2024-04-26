using System;
using System.Collections.Generic;
using System.Linq;

namespace FribergBlazorApp.Helpers
{
    public class SearchFilter<T>
    {
        private IEnumerable<T> _sourceList;
        private IEnumerable<T> _filteredItems;
        private Func<T, string> _propertySelector;
        private string _searchTerm;

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                ApplyFilter();
            }
        }

        public SearchFilter(IEnumerable<T> sourceList, Func<T, string> propertySelector)
        {
            _sourceList = sourceList;
            _propertySelector = propertySelector;
            _filteredItems = _sourceList;
            _searchTerm = string.Empty;
        }

        public IEnumerable<T> FilteredItems => _filteredItems;

        private void ApplyFilter()
        {
            if (string.IsNullOrWhiteSpace(_searchTerm))
            {
                _filteredItems = _sourceList;
            }
            else
            {
                _filteredItems = _sourceList.Where(item => IsMatch(item));
            }
        }

        private bool IsMatch(T item)
        {
            var propertyValue = _propertySelector(item);
            return propertyValue?.IndexOf(_searchTerm, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
