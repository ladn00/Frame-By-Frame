//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Фотостудия
{
    using System;
    using System.Collections.Generic;
    
    public partial class Локация
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Локация()
        {
            this.Договор = new HashSet<Договор>();
        }
    
        public int Номер_локации { get; set; }
        public string Адрес { get; set; }
        public string Изображение { get; set; }
        public string Во_владении_компании { get; set; }

        public string ImageForWins
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + "..\\..\\" + "Pages/imgs/" + Изображение;
            }
        }
        public string ActualImage
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + "..\\..\\" + "Pages/imgs/" + Изображение;
            }
        }
        public string ActualAddress
        {
            get
            {
                return Адрес.Trim();
            }
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Договор> Договор { get; set; }
    }
}
