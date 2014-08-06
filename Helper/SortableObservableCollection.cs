using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Documents;

namespace PrototypeEDUCOM.Helper
{

    /// <filename>Dictionaries.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Etend la classe ObservableCollection, afin d'y ajouter la fonction de tri sur les éléments de la collection</summary>
    public class SortableObservableCollection<T> : ObservableCollection<T>
    {
        public SortableObservableCollection(List<T> list) : base(list) {}

        /// <summary>
        /// Permet de trier la collection en fonction d'un élément de la collection
        /// </summary>
        /// <typeparam name="TKey">Le type d'objet de la collection</typeparam>
        /// <param name="keySelector">l'atribut de tri</param>
        /// <param name="direction">Le sens du tri</param>
        public void Sort<TKey>(Func<T, TKey> keySelector, System.ComponentModel.ListSortDirection direction)
        {
            switch (direction)
            {
                case System.ComponentModel.ListSortDirection.Ascending:
                    {
                        ApplySort(Items.OrderBy(keySelector));
                        break;
                    }
                case System.ComponentModel.ListSortDirection.Descending:
                    {
                        ApplySort(Items.OrderByDescending(keySelector));
                        break;
                    }
            }
        }

        /// <summary>
        /// Effectue la permutation des éléments dana la liste
        /// </summary>
        /// <param name="sortedItems">Le paramètre de tri</param>
        private void ApplySort(IEnumerable<T> sortedItems)
        {
            var sortedItemsList = sortedItems.ToList();

            foreach (var item in sortedItemsList)
            {
                Move(IndexOf(item), sortedItemsList.IndexOf(item));
            }
        }
    }

}
