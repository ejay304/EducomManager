using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisations
{
    /// <filename>ShowOrganisationViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère le contrôle utilisateur qui affiche le détails d'une organisation</summary>
    class ShowOrganisationViewModel : BaseViewModel
    {

        public Organisation organisation { get; set; }
        public ObservableCollection<Program> programs { get; set; }

        public ICommand cmdEdit { get; set; }
        public ICommand cmdDelete { get; set; }
        public ICommand cmdAddProgram { get; set; }
        public ICommand cmdShowProgram { get; set; }

        /// <summary>
        /// Initialise les valeurs à bindet, les les commandes aux actions concernée 
        /// S'abonne aux événements le concernant
        /// </summary>
        /// <param name="organisation">L'organisaiton concernée</param>
        public ShowOrganisationViewModel(Organisation organisation)
        {
            this.organisation = organisation;
            this.programs = new ObservableCollection<Program>(organisation.programs.ToList());

            this.cmdEdit = new RelayCommand<Organisation>(actEdit);
            this.cmdDelete = new RelayCommand<Organisation>(actDelete);
            this.cmdAddProgram = new RelayCommand<Organisation>(actAddProgram);
            this.cmdShowProgram = new RelayCommand<Program>(actShowProgram);

            mediator.Register(Helper.Event.ADD_PROGRAM, this);
            mediator.Register(Helper.Event.DELETE_PROGRAM, this);
        }

        /// <summary>
        /// Ouvre la fenêtre d'édition d'organisation
        /// </summary>
        /// <param name="o></param>
        private void actEdit(Object o)
        {
            mediator.openEditOrganisationView(this.organisation);
        }

        /// <summary>
        /// Ouvre la fenêtre de suppression d'organisation
        /// </summary>
        /// <param name="o"></param>
        private void actDelete(Object o)
        {
            mediator.openDeleteOrganisationView(this.organisation);
        }

        /// <summary>
        /// Ouvre la fenêtre d'ajout de programme
        /// </summary>
        /// <param name="o"></param>
        private void actAddProgram(Object o)
        {
            mediator.openAddProgramView(this.organisation);
        }
        
        /// <summary>
        /// Ouvre l'onglet programme
        /// </summary>
        /// <param name="program">le programme concerné</param>
        private void actShowProgram(Program program)
        {
            mediator.openShowProgramView(program);

        }

        /// <summary>
        /// Fonction de mise à jour en cas de notification d'événement
        /// </summary>
        /// <param name="eventName">Le type d'événement</param>
        /// <param name="item">l'objet concerné par l'événement</param>
        public override void Update(string eventName, object item)
        {
            switch (eventName)
            {
                case Helper.Event.ADD_PROGRAM:
                    this.programs.Add((Program)item);
                    NotifyPropertyChanged("programs");
                    break;

                case Helper.Event.DELETE_PROGRAM:
                    this.programs.Remove((Program)item);
                    NotifyPropertyChanged("programs");
                    break;
            }
        }

    }
}
