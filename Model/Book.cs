using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newTestLibrary.Model
{
    public class Book
    {
		private int id;

		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		private string title;

		public string Title
		{
			get { return title; }
			set { title = value; }
		}
		private string author;

		public string Author
		{
			get { return author; }
			set { author = value; }
		}
		private DateTime release;

		public DateTime Release
		{
			get { return release; }
			set { release = value; }
		}
		private string genre;		

		public string Genre
		{
			get { return genre; }
			set { genre = value; }
		}
        public Book()
        {
            
        }





    }
}
