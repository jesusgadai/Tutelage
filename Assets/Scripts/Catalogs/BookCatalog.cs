using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BookCatalog : MonoBehaviour
{
    public List<Book> books = new List<Book>();

    #region Singleton
    public static BookCatalog instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("Multiple BookCatalog(s) found!");
            Destroy(gameObject);
        }

        LoadResources();
    }
    #endregion

    void LoadResources()
    {
        books = Resources.LoadAll<Book>("Books").OfType<Book>().ToList();
    }

    public List<Book> GetAllBooks()
    {
        return books;
    }

    public Book FindBook(string bookTitle)
    {
        try
        {
            return books.Find(r => r.title.Equals(bookTitle));
        }
        catch
        {
            return null;
        }
    }
}
