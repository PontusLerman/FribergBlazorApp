
//author: Christian Alp
namespace FribergBlazorApp.Helpers
{
	public class SearchFilter<T>
	{
		private readonly IEnumerable<T> _sourceList;
		private IEnumerable<T> _filteredItems;
		private List<Func<T, string>> _propertySelectors;
		public string SearchTerm { get; set;}

		public SearchFilter(IEnumerable<T> sourceList, params Func<T, string>[] propertySelectors)
		{
			_sourceList = sourceList;
			_propertySelectors = propertySelectors.ToList();
			_filteredItems = _sourceList;
		}

		public IEnumerable<T> ApplyFilter()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                _filteredItems = _sourceList;
            }
            else
            {
                _filteredItems = _sourceList.Where(IsMatch);
            }
            return _filteredItems;
        }

        private bool IsMatch(T item)
        {
            foreach (var propertySelector in _propertySelectors)
            {
                var propertyValue = propertySelector(item);
                if (propertyValue?.IndexOf(SearchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
	}
}
