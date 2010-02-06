/*
FED (Front-End for Dosbox), a gaming graphical user interface for the DOSBox emulator.
Copyright (C) 2009  Ka Ki Cheung

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Plain.Design {
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Event, AllowMultiple = false, Inherited = false)]
	public class CategoryOrderAttribute : System.ComponentModel.CategoryAttribute {
		static CategoryOrderAttribute() {
			s_CategoryToOrder = new Dictionary<string, int>();
		}
		const string PREFIX_FOR_ORDER = "\u001f";
		// NOTE: Obviously this has the problem of same attribute 
		//	describing different targets overriding the order, but
		//	it's a balance between that and ease of use.
		static Dictionary<string, int> s_CategoryToOrder;
		static string CategoryOrder(string category) {
			int order;
			if (s_CategoryToOrder.TryGetValue(category, out order)) {
				return PREFIX_FOR_ORDER + (char) ('A' + order) + category;
			}
			else {
				return category;
			}
		}
		static string CategoryOrder(string category, int order) {
			if (s_CategoryToOrder.ContainsKey(category)) {
				s_CategoryToOrder[category] = order;
			}
			else {
				s_CategoryToOrder.Add(category, order);
			}
			return CategoryOrder(category);
		}

		public CategoryOrderAttribute()
			: base() {
			m_Category = base.Category;
		}
		/// <summary>
		/// Initializes a new instance of the CategoryOrderAttribute class using the specified category name and previously specified order, if any.
		/// </summary>
		/// <param name="category">The name of the category.</param>
		public CategoryOrderAttribute(string category)
			: base(CategoryOrder(category)) {
			m_Category = category;
		}
		/// <summary>
		/// Initializes a new instance of the CategoryOrderAttribute class using the specified category name and order.
		/// </summary>
		/// <param name="category">The name of the category.</param>
		/// <param name="order">The order of the category.</param>
		public CategoryOrderAttribute(string category, int order)
			: this(category, order, string.Empty) { }
		/// <summary>
		/// Initializes a new instance of the CategoryOrderAttribute class using the specified category name and order.
		/// </summary>
		/// <param name="category">The name of the category.</param>
		/// <param name="order">The order of the category.</param>
		/// <param name="description">The description of the category.</param>
		public CategoryOrderAttribute(string category, int order, string description)
			: base(CategoryOrder(category, order)) {
			m_Category = category;
			m_Description = description;
		}
		/// <summary>
		/// Gets the name of the category for the property or event that this attribute is applied to.
		/// </summary>
		public new string Category {
			get { return m_Category; }
		}
		/// <summary>
		/// Gets the description of the category for the property or event that this attribute is applied to.
		/// </summary>
		public string Description {
			get { return m_Description; }
		}

		string m_Category;
		string m_Description;
	}
}
