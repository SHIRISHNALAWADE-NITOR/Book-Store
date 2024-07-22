using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class BookFile
{
    [Key]
    public int Id { get; set; }

    public byte[] AudioFile { get; set; }

    public byte[] VideoFile { get; set; }

    public byte[] PdfFile { get; set; }

    public int BookId { get; set; }

    [ForeignKey("BookId")]
    public Book Book { get; set; }
}