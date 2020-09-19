namespace DAL
{
	public interface IUnitOfWork : System.IDisposable
	{
		IPersonRepository PersonRepository { get; }
		IBookRepository BookRepository { get; }
		IBookTypeRepository BookTypeRepository { get; }
		IFieldOfStudyRepository FieldOfStudyRepository { get; }
		ISectionRepository SectionRepository { get; }
		ILessonRepository LessonRepository { get; }
		IProductTypeRepository ProductTypeRepository { get; }
		IPageRepository PageRepository { get; }
		//IPageBaseRepository PageBaseRepository { get; }
		void Save();
	}
}
