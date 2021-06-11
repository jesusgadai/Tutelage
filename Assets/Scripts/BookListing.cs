using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookListing : MonoBehaviour
{
    public Image cover;
    public TMP_Text title;
    public TMP_Text summary;

    public void SetBook(Book book)
    {
        cover.sprite = book.cover;
        title.text = book.title;
        summary.text = book.summary;
    }
}
