using MVVM_Simple_Book.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Simple_Book.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        Book[] books;
        Book selectedBook;

        public Book[] Books
        { get; private set; }

        // храним выбранную в форме книгу
        public Book SelectedBook
        { get
            {
                return selectedBook;
            }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }
        // ниже расширенный вариант
        //{
        //    get
        //    {
        //        return books;
        //    }
        //    set
        //    {

        //        OnPropertyChanged("Books");
        //    }
        //}

        // создаем конструктор
        public MainWindowViewModel()
        {
            Books = Book.GetBooks();
        }


        // создаем интерфейс по анналогии с классом Book
        public event PropertyChangedEventHandler PropertyChanged;

        // создаем отдельный метод для проверки изменения свойств всесто того чтобы делать это для каждого свойства и дублировать код
        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
