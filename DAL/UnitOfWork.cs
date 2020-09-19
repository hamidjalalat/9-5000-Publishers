namespace DAL
{
	public class UnitOfWork : System.Object, IUnitOfWork
	{
		public UnitOfWork()
		{
			IsDisposed = false;
		}

		protected bool IsDisposed { get; set; }

		private Models.DatabaseContext _databaseContext;

		protected virtual Models.DatabaseContext DataBaseContext
		{
			get
			{
				if (_databaseContext == null)
				{
					_databaseContext =
						new Models.DatabaseContext();
				}

				return (_databaseContext);
			}
		}

		// **************************************************
		//private IXXXXXRepository _XXXXXRepository;

		//public IXXXXXRepository XXXXXRepository
		//{
		//	get
		//	{
		//		if (_XXXXXRepository == null)
		//		{
		//			_XXXXXRepository =
		//				new XXXXXRepository(DataBaseContext);
		//		}

		//		return (_XXXXXRepository);
		//	}
		//}
		// **************************************************

		// **************************************************
		//private IPageBaseRepository _pageBaseRepository;

		//public IPageBaseRepository PageBaseRepository
		//{
		//	get
		//	{
		//		if (_pageBaseRepository == null)
		//		{
		//			_pageBaseRepository =
		//				new PageBaseRepository(DataBaseContext);
		//		}

		//		return (_pageBaseRepository);
		//	}
		//}
		// **************************************************


		// **************************************************
		private IPageRepository _pageRepository;

		public IPageRepository PageRepository
		{
			get
			{
				if (_pageRepository == null)
				{
					_pageRepository =
						new PageRepository(DataBaseContext);
				}

				return (_pageRepository);
			}
		}
		// **************************************************

		// **************************************************
		private IProductTypeRepository _productTypeRepository;

		public IProductTypeRepository ProductTypeRepository
		{
			get
			{
				if (_productTypeRepository == null)
				{
					_productTypeRepository =
						new ProductTypeRepository(DataBaseContext);
				}

				return (_productTypeRepository);
			}
		}
		// **************************************************


		// **************************************************
		private ILessonRepository _lessonRepository;

		public ILessonRepository LessonRepository
		{
			get
			{
				if (_lessonRepository == null)
				{
					_lessonRepository =
						new LessonRepository(DataBaseContext);
				}

				return (_lessonRepository);
			}
		}
		// **************************************************



		// **************************************************
		private ISectionRepository _sectionRepository;

		public ISectionRepository SectionRepository
		{
			get
			{
				if (_sectionRepository == null)
				{
					_sectionRepository =
						new SectionRepository(DataBaseContext);
				}

				return (_sectionRepository);
			}
		}
		// **************************************************


		//**************************************************
		private IFieldOfStudyRepository _fieldOfStudyRepository;

		public IFieldOfStudyRepository FieldOfStudyRepository
		{
			get
			{
				if (_fieldOfStudyRepository == null)
				{
					_fieldOfStudyRepository =
						new FieldOfStudyRepository(DataBaseContext);
				}

				return (_fieldOfStudyRepository);
			}
		}
		 //**************************************************

		// **************************************************
		private IBookTypeRepository _bookTypeRepository;

		public IBookTypeRepository BookTypeRepository
		{
			get
			{
				if (_bookTypeRepository == null)
				{
					_bookTypeRepository =
						new BookTypeRepository(DataBaseContext);
				}

				return (_bookTypeRepository);
			}
		}
		 //**************************************************




		// **************************************************

		// **************************************************
		private IBookRepository _BookRepository;

		public IBookRepository BookRepository
		{
			get
			{
				if (_BookRepository == null)
				{
					_BookRepository =
						new BookRepository(DataBaseContext);
				}

				return (_BookRepository);
			}
		}
		 //**************************************************



		// **************************************************
		private IPersonRepository _personRepository;

		public IPersonRepository PersonRepository
		{
			get
			{
				if (_personRepository == null)
				{
					_personRepository =
						new PersonRepository(DataBaseContext);
				}

				return (_personRepository);
			}
		}

		public void Save()
		{
			DataBaseContext.SaveChanges();
		}

		protected virtual void Dispose(bool disposing)
		{
			if (IsDisposed == false)
			{
				if (disposing)
				{
					_databaseContext.Dispose();
					_databaseContext = null;
				}
			}

			IsDisposed = true;
		}

		public void Dispose()
		{
			Dispose(true);

			System.GC.SuppressFinalize(this);
		}
	}
}
