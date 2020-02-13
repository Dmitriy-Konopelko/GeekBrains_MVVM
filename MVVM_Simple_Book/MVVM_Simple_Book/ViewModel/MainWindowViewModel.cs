using MVVM_Simple_Book.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVM_Simple_Book.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        Book[] books;
        Book selectedBook;

        // первоначальный вариант создаем массив
        //public Book[] Books
        //{ get; private set; }
        // второй вариант создаем коллекцию
        public ObservableCollection <Book> Books { get; private set; }

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

        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }


        // создаем конструктор
        public MainWindowViewModel()
        {
            // первоначальный вариант при использовании массива
            //Books = Book.GetBooks();
            // второй вариант при использовании коллекции
            Books = new ObservableCollection<Book>(Book.GetBooks());
            AddCommand = new DelegateCommand(AddBook);
            RemoveCommand = new DelegateCommand(RemoveBook, canRemoveBook);
        }

        // добавляем команду
        void AddBook(object obj)
        {
            //MessageBox.Show("Добавлена книга", "Внимание!", MessageBoxButton.OK);
            Books.Add(new Book {Author = "Автор", Title = "Новая книга"});
        }

        void RemoveBook(object obj)
        {
            Books.Remove((Book)obj);
        }

        bool canRemoveBook(object arg)
        {
            return (arg as Book) != null;
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
