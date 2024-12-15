using System.Text.Json;

namespace MyMLApp
{
    public class Guestbook // klass f�r att skapa g�stbok
    {
        private string filename = @"guestbook.json"; // filnamn f�r att spara g�stboksinl�gg
        private List<GuestbookEntry> guestbookEntries = new List<GuestbookEntry>(); // lista f�r att spara inl�gg i g�stboken

        public Guestbook()
        { // konstruktor f�r att skapa g�stbok
            if (File.Exists(filename)==true){ // If stored json data exists then read
                string jsonString = File.ReadAllText(filename); // l�ser in json data fr�n fil
                guestbookEntries = JsonSerializer.Deserialize<List<GuestbookEntry>>(jsonString)!; // deserialiserar json data till lista
            }
        }
        public GuestbookEntry AddGuestbookEntry(string name, string comment, char assessment)
        { // metod f�r att l�gga till inl�gg i g�stboken
            GuestbookEntry obj = new GuestbookEntry(); // skapar nytt objekt av klassen GuestbookEntry
            obj.Comment = comment; // s�tter kommentar
            obj.Name = name; // s�tter namn
            obj.Assessment= assessment; // s�tter bed�mning
            guestbookEntries.Add(obj);// l�gger till objekt i listan
            Marshal();// serialiserar objekt och sparar till fil
            return obj; // returnerar objekt
        }
        
        public int DelEntry(int index)
        { // metod f�r att ta bort inl�gg i g�stboken
            guestbookEntries.RemoveAt(index); // tar bort inl�gg fr�n listan
            Marshal(); // serialiserar objekt och sparar till fil
            return index; // returnerar index
        }

        public int Length()
        {
            int length = guestbookEntries.Count; // r�knar antal inl�gg i g�stboken
            return length; // returnerar antal inl�gg
        }

        public (int positiveRemarks, int length, int approvalRate) ApprovalData() // metod f�r att r�kna ut godk�nnandegrad etc
        {
            int length = this.Length(); // r�knar antal inl�gg i g�stboken
            int positiveRemarks = 0; 

            foreach (var entry in guestbookEntries) // loopar igenom inl�gg i g�stboken
            {
                if (entry.Assessment == '+') // om bed�mning �r positiv
                {
                    positiveRemarks++; // �kar r�knare f�r positiva inl�gg
                }
            }
            int approvalRate = (int)Math.Round(((double)positiveRemarks / length) * 100, 0); // r�knar ut godk�nnandegrad

            return (positiveRemarks, length, approvalRate); // returnerar antal positiva inl�gg, antal inl�gg och godk�nnandegrad
        }

        public List<GuestbookEntry> GetGuestbookEntries()
        { // metod f�r att h�mta inl�gg i g�stboken
            return guestbookEntries; // returnerar lista med inl�gg
        }

        private void Marshal() // metod f�r att serialisera objekt och spara till fil
        {
            // Serialize all the objects and save to file
            var jsonString = JsonSerializer.Serialize(guestbookEntries);// serialiserar lista med objekt till json
            File.WriteAllText(filename, jsonString); // skriver json data till fil
        }
    }
}

//bygger v�ldigt mycket p� exempelfil fr�n f�rel�sning