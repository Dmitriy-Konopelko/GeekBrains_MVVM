using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Simple_Book.Model
{
    public class Book : INotifyPropertyChanged
    {
        string author;
        string title;
        
        public string Author 
        {
            get 
            {
                return author;
            }
            set
            {
                author = value;
                OnPropertyChanged("Author");
            }
        }

        public string Title 
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        // создаем массив книг
        public static Book[] GetBooks ()
        {
            var result = new[]
            {
                new Book () { Author = "Лев Толстой", Title = "Война и мир"},
                new Book () { Author = "Михаил Булгаков", Title = "Мастер и Маргарита"},
                new Book () { Author = "Стивен Кинг", Title = "Оно"}
            };
            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // создаем отдельный метод для проверки изменения свойств всесто того чтобы делать это для каждого свойства и дублировать код
        void OnPropertyChanged (string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
    }
}
