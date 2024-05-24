namespace help_reviews.Interfaces;
// NOTE interface is a contract, whenever a type implements an interface, it must support the listed members in the definition
public interface IRepository<T>
{
	public List<T> GetAll();
	public T Create(T data);
	public T GetById(int id);
	public T Update(T data);
	public void Destroy(int id);
}



// THIS FILE IS NOT CURRENTLY BEING USED. JUST SITTING HERE IF YOU WANT TO IMPLEMENT IT.