using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Reading : MonoBehaviour
{
    [Header("Screens")]
    public GameObject preReadingNavigation;
    public GameObject playAudioReaderBtn;
    public GameObject readingCountdown;
    public GameObject postReadingDetails;
    public GameObject postReadingNavigation;

    [Header("Book Selection")]
    public ScrollSnapRect bookSelection;
    public Transform bookSelectionParant;
    public BookListing bookListing;
    Book selectedBook;

    [Header("Text To Speech")]
    public TTSController tTSController;
    public UnityEvent onTTSDone;

    BookCatalog bookCatalog;
    bool instanceSet = false;

    void OnEnable()
    {
        preReadingNavigation.SetActive(true);
        playAudioReaderBtn.SetActive(false);
        readingCountdown.SetActive(false);
        postReadingDetails.SetActive(false);
        postReadingNavigation.SetActive(false);

        if (instanceSet)
            tTSController.ResetStopInvoked();
    }

    void Start()
    {
        bookCatalog = BookCatalog.instance;
        tTSController.onSpeakStop.AddListener(OnTTSDone);
        instanceSet = true;

        PopulateBooks();
    }

    void PopulateBooks()
    {
        foreach (Book book in bookCatalog.GetAllBooks())
        {
            BookListing newListing = Instantiate(bookListing, Vector3.zero, Quaternion.identity, bookSelectionParant);
            newListing.SetBook(book);
        }
    }

    public void SetSelectedBook()
    {
        selectedBook = bookCatalog.GetAllBooks()[bookSelection._currentPage];
        playAudioReaderBtn.SetActive(true);
    }

    public void PlayTTS()
    {
        tTSController.StartSpeak(selectedBook.story);
    }

    void OnTTSDone()
    {
        if (onTTSDone != null)
            onTTSDone.Invoke();
    }

    void OnDisable()
    {
        tTSController.StopSpeak();
    }
}
