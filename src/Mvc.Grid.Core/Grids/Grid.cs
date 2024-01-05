using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NonFactors.Mvc.Grid
{
	public class Grid<T> : IGrid<T> where T : class
	{
		public String Url { get; set; }
		public String? Id { get; set; }
		public String Name { get; set; }
		public String? EmptyText { get; set; }

		public IGridSort<T> Sort { get; set; }
		public IQueryable<T> Source { get; set; }
		public IQueryCollection? Query { get; set; }
		public GridProcessingMode Mode { get; set; }
		public ViewContext? ViewContext { get; set; }
		public GridFilterMode FilterMode { get; set; }
		public String FooterPartialViewName { get; set; }
		public GridHtmlAttributes Attributes { get; set; }
		public HashSet<IGridProcessor<T>> Processors { get; set; }

		IGridColumns<IGridColumn> IGrid.Columns => Columns;
		public IGridColumnsOf<T> Columns { get; set; }

		IGridRows<Object> IGrid.Rows => Rows;

		public IGridRowsOf<T> Rows { get; set; }

		IGridPager? IGrid.Pager => Pager;
		public IGridPager<T>? Pager { get; set; }

		public Grid(IEnumerable<T> source)
		{
			Url = "";
			Name = "";
			FooterPartialViewName = "";
			Source = source.AsQueryable();
			FilterMode = GridFilterMode.Excel;
			Mode = GridProcessingMode.Automatic;
			Attributes = new GridHtmlAttributes();	
			Processors = new HashSet<IGridProcessor<T>>();

			Columns = new GridColumns<T>(this);
			Rows = new GridRows<T>(this);
			Sort = new GridSort<T>(this);
		}

		#region X600

		int? _totalRowsCount = null;

		public bool UseCustomPaging
		{
			get
			{
				return _totalRowsCount != null;
			}
		}

		public int TotalRowsCount
		{
			get
			{
				if (_totalRowsCount != null) return _totalRowsCount.Value;
				return Source.Count();// Rows.Count();
			}
			set
			{
				if (value >= Rows.Count())
				{
					_totalRowsCount = value;
				}
				else
				{
					_totalRowsCount = null;
				}
			}
		}

		public int CurrentPage
		{
			get
			{
				if (Pager != null)
					return Pager.CurrentPage;
				else
					return 1;
			}
		}

		public int MinRowNumber
		{
			get
			{
				if (Pager == null) return 1;
				return (CurrentPage - 1) * Pager.RowsPerPage + 1;
			}
		}

		public int MaxRowNumber
		{
			get
			{
				if (Pager == null || TotalRowsCount < Pager.RowsPerPage)
				{
					return TotalRowsCount;
				}

				if (CurrentPage * Pager.RowsPerPage < TotalRowsCount)
				{
					return CurrentPage * Pager.RowsPerPage;
				}
				return TotalRowsCount;
			}
		}

		public bool IsPagerVisible
		{
			get
			{
				return Pager != null && TotalRowsCount > Pager.RowsPerPage;
			}
		}

		#endregion
	}
}
