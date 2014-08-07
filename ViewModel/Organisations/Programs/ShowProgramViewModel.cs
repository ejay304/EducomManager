using PrototypeEDUCOM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrototypeEDUCOM.ViewModel.Organisations.Programs
{
    /// <filename>ShowProgramViewModel.cs</filename>
    /// <author>Alain FRESCO</author>
    /// <author>Romain THERISOD</author>
    /// <date>01/08/2014 </date>
    /// <summary>Classe de type ViewModel, qui gère le contrôle utilisateur qui affiche le détails d'un programme</summary>
    class ShowProgramViewModel : BaseViewModel
    {
        public Program program { get; set; }
        public ObservableCollection<Campus> campuses { get; set; }
        public bool editDescription { get; set; }
        public string description { get; set; }

        public ICommand cmdEdit { get; set; }
        public ICommand cmdDelete { get; set; }
        public ICommand cmdAddCampus { get; set; }
        public ICommand cmdEditCampus { get; set; }
        public ICommand cmdDeleteCampus { get; set; }
        public ICommand cmdEditDescription { get; set; }
    

    
        /// <summary>
        /// Initialise la liste des valeurs à bindé, lie les commandes aux actions concernées,
        /// s'abonne à l'ajout de campus
        /// </summary>
        /// <param name="program">le programme concerné</param>
        public ShowProgramViewModel(Program program) {

            this.program = program;
            this.description = program.description;

            this.campuses = new ObservableCollection<Campus>(program.campus.ToList());
            this.cmdEdit = new RelayCommand<Object>(actEdit);
            this.cmdDelete = new RelayCommand<Object>(actDelete);

            this.cmdEditDescription = new RelayCommand<Object>(actEditDescription);
            this.editDescription = false;

            this.cmdAddCampus = new RelayCommand<Object>(actAddCampus);
            this.cmdEditCampus = new RelayCommand<Campus>(actEditCampus);
            this.cmdDeleteCampus = new RelayCommand<Campus>(actDeleteCampus);

            mediator.Register(Helper.Event.ADD_CAMPUS, this);
        }

        /// <summary>
        /// Ouvre la fenêtre d'édition de programmes
        /// </summary>
        /// <param name="o"></param>
        public void actEdit(Object o){
            mediator.openEditProgramView(this.program);
        }

        /// <summary>
        /// Ouvre la fenêtre de suppression de programmes
        /// </summary>
        /// <param name="o"></param>
        public void actDelete(Object o){
            mediator.openDeleteProgramView(this.program);
        }

        /// <summary>
        /// Gère la modificaiton de la decription du programme
        /// </summary>
        /// <param name="o"></param>
        public void actEditDescription(Object o)
        {
            if (editDescription)
            {
                program.description = description;
                db.SaveChanges();
            }

            editDescription = !editDescription;
            NotifyPropertyChanged("editDescription");
        }

        /// <summary>
        /// Ouvre la fenêtre d'ajout de campus
        /// </summary>
        /// <param name="o"></param>
        public void actAddCampus(Object o) {
            mediator.openAddCampusView(program);
        }

        /// <summary>
        /// Ouvre la fenêtre d'édition du campus
        /// </summary>
        /// <param name="campus"></param>
        public void actEditCampus(Campus campus)
        {
            mediator.openEditCampusView(campus);
        }

        /// <summary>
        /// Ouvre la fenêtre de suppression de campus
        /// </summary>
        /// <param name="campus"></param>
        public void actDeleteCampus(Campus campus) {
            mediator.openDeleteCampusView(campus);
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
                case Helper.Event.ADD_CAMPUS:
                    this.campuses.Add((Campus)item);
                    NotifyPropertyChanged("campuses");
                    break;

                case Helper.Event.DELETE_CAMPUS:
                    this.campuses.Remove((Campus)item);
                    NotifyPropertyChanged("campuses");
                    break;
            }
        }
    }
}
