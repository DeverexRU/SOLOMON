using SolomonDesktop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SolomonDesktop.ViewModels
{
    /// <summary>
    /// Форма ввода материалов для решения.
    /// Класс FormSDIGECatalog - ViewModel. Взаимодействие данных программы и формы
    /// </summary>
    public class MainForm : INotifyPropertyChanged
    {
        #region Данные

        /// <summary>
        /// Каталог
        /// </summary>
        public ObservableCollection<object> TopLevelCards { get; set; }
        //public object СatConstr { get; private set; }

        /// <summary>
        /// Слежение за текущим индексом
        /// </summary>
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set { _SelectedIndex = value; OnPropertyChanged(); }
        }
        private int _SelectedIndex = -1;

        /// <summary>
        /// Коснтруктор этой VierwModel
        /// </summary>
        public MainForm()
        {
            TopLevelCards = new ObservableCollection<object>();
            //загрузка копии данных из Model
            LoadFromModel();
        }

        /// <summary>
        /// Объявление свойства из INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Команды

        public RelayCommand CmdAddItemBlokbox { get { return new RelayCommand(_DoAddItemBlokbox, _AlwaysTrue); } }
        public RelayCommand CmdAddItemEmkost { get { return new RelayCommand(_DoAddItemEmkost, _AlwaysTrue); } }
        public RelayCommand CmdAddItemOpora { get { return new RelayCommand(_DoAddItemOpora, _AlwaysTrue); } }
        public RelayCommand CmdAddItemSvay { get { return new RelayCommand(_DoAddItemSvaya, _AlwaysTrue); } }
        public RelayCommand CmdEditItem { get { return new RelayCommand(_DoEditItem, _ItemsCommandsEnabled); } }
        public RelayCommand CmdDeleteItem { get { return new RelayCommand(_DoDeleteItem, _ItemsCommandsEnabled); } }

        private bool _AlwaysTrue() { return true; }
        private bool _AlwaysFalse() { return false; }

        #endregion


        #region Commands - реализация

        private void _DoAddItemBlokbox()
        {
            //DLog.Ln("_DoAddItemBlokbox()");
            if (new FormDialog1().ShowDialog() == true)
            {
                //DLog.Ln("FormCatConstrBlokbox(-1).ShowDialog() == true");
                LoadFromModel();
            }
        }

        private void _DoAddItemEmkost()
        {
            //if (new FormConstrEmkost(-1).ShowDialog() == true)
            //{
            //    LoadFromModel();
            //}
        }

        private void _DoAddItemOpora()
        {
            //if (new FormConstrOpora(-1).ShowDialog() == true)
            //{
            //    LoadFromModel();
            //}
        }

        private void _DoAddItemSvaya()
        {
            DLog.Ln("_DoAddItemSvaya()");
            if (new FormCatConstrSvaya(-1).ShowDialog() == true)
            {
                DLog.Ln("FormCatConstrSvaya(-1).ShowDialog() == true");
                LoadFromModel();
            }
        }

        private void _DoEditItem()
        {
            switch (CatConstr[_SelectedIndex].VConstrElemKind)
            {
                case MDConstrElemKind.blokbox:
                    if (new FormCatConstrBlokbox(SelectedIndex).ShowDialog() == true)
                    {
                        LoadFromModel();
                    }
                    break;
            }
        }

        private void _DoDeleteItem()
        {
            if (MessageBox.Show(
                "Удалить выделенную строку с данными?",
                "Удаление данных ИГЭ",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //выполнить удаление текущей строки - получить номер и грохнуть её
                CatConstr.RemoveAt(SelectedIndex);
            }
        }

        private bool _ItemsCommandsEnabled()
        {
            return (_SelectedIndex != -1);
        }

        #endregion


        #region Методы копирования данных

        /// <summary>
        /// Копирование данных из основного хранилища в текущую ViewModel
        /// </summary>
        public void LoadFromModel()
        {
            //считать в таблицу только общие свойства
            CatConstr.Clear();
            //проход по строкам данных ИГЭ
            foreach (var v in Ap.MC.fModelConstr.fConstrElements)
            {
                //создать строку параметров в VM
                CatConstr.Add(new VM_CatConstrItem()
                {
                    VConstrElemKind = v.fConstrElemKind,
                    VElemShifr = v.fElemShifr,
                    VElemCaption = v.fElemCaption,
                    VResponsibilityLevel = v.fResponsibilityLevel,
                    VBeginExploitation = v.fBeginExploitation,
                    VExploitationTerm = v.fExploitationTerm
                });
            }
        }

        /// <summary>
        /// копирование данных из текущей ViewModel в основное хранилище 
        /// </summary>
        public void SaveToModel()
        {
            ////предзачистка данных в хранилище
            //Ap.MC.fModelGeo.fIGEProps.Clear();
            ////???????
            //foreach (var v in IGECatalog)
            //{
            //    Ap.MC.fModelGeo.fIGEProps.Add(new LibNormDoc.SP25133302012i1.Appendix2.IGEProperties()
            //    {
            //        //заполнить
            //        //TODO:!!!!!!!!!!!!!!!!! проверить!!!
            //        fShifr = v.VShifr,
            //        //mpl.v01_GrTp = dl.VGrTp; //переделать!!!!!!!!!!!!!!!
            //        Tbf = v.VTbf,
            //        LMf = v.VLMf,
            //        LMth = v.VLMth,
            //        Cf = v.VCf,
            //        Cth = v.VCth,
            //        Wtot = v.VWtot,
            //        Rod = v.VRod,
            //        Wp = v.VWp,
            //        Ip = v.VIp,
            //        Dsal = v.VDsal
            //    });
            //}
        }

        #endregion


        }

    }
}
