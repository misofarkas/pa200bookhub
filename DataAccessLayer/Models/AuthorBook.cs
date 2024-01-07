using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models;

   public class AuthorBook : BaseEntity
   {
    public int AuthorId { get; set; }
    public Author Author { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; }
   }

