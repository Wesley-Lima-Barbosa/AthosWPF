using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AthosWPF.Service;
using AthosWPF.Model;
using System.Data;

namespace AthosWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        DataTable dt;

        private void Listar_Click(object sender, RoutedEventArgs e)
        {

        }


        private async void Usuarios_Click(object sender, RoutedEventArgs e)
        {
            //Faz o formulário de envio de e-mail não seja mais visto
            EnviarForm.Visibility = Visibility.Collapsed;
            //Faz o datagrid ser vísivel
            dtGrid.Visibility = Visibility.Visible;

            //Cria um novo service para consumir a API
            UsuarioService usuarioService = new UsuarioService();

            //Cria uma nova lista de usuarios
            List<Usuario> usuarioList = new List<Usuario>();

            //Consume a API utilizando o service e manda para a lista o resultado
            usuarioList = await usuarioService.UsuarioListar();

            //Cria um novo data table e novas colunas, depois adiciona as colunas no data table 
            dt = new DataTable();
            DataColumn nome = new DataColumn("Nome", typeof(string));
            DataColumn email = new DataColumn("E-mail", typeof(string));
            DataColumn condominio = new DataColumn("Condomínio", typeof(string));

            dt.Columns.Add(nome);
            dt.Columns.Add(email);
            dt.Columns.Add(condominio);

            //Para cada item na lista de usuários, será adicionado uma novo item no DataTable
            foreach (var item in usuarioList)
            {
                DataRow row = dt.NewRow();
                row[0] = item.Nome;
                row[1] = item.Email;
                row[2] = item.Condominio.NomeCondominio;

                dt.Rows.Add(row);


            }

            dtGrid.ItemsSource = dt.DefaultView;


        }

        private async void Administradoras_Click(object sender, RoutedEventArgs e)
        {
            dt = new DataTable();
            EnviarForm.Visibility = Visibility.Collapsed;

            dtGrid.Visibility = Visibility.Visible;
            UsuarioService usuarioService = new UsuarioService();
            List<Administradora> administradoraList = new List<Administradora>();
            administradoraList = await usuarioService.AdministradoraListar();


            DataColumn nome = new DataColumn("Nome", typeof(string));

            dt.Columns.Add(nome);

            foreach (var item in administradoraList)
            {
                DataRow row = dt.NewRow();
                row[0] = item.NomeAdministradora;
      

                dt.Rows.Add(row);


            }





            dtGrid.ItemsSource = dt.DefaultView;

        }

        private async void Condominios_Click(object sender, RoutedEventArgs e)
        {
            UsuarioService service = new UsuarioService();

            dt = new DataTable();
            EnviarForm.Visibility = Visibility.Collapsed;

            dtGrid.Visibility = Visibility.Visible;
            UsuarioService usuarioService = new UsuarioService();
            List<Condominio> condominioList = new List<Condominio>();
            condominioList = await usuarioService.CondominioListar();

            DataColumn nome = new DataColumn("Nome", typeof(string));
            DataColumn administradora = new DataColumn("Administradora", typeof(string));
            dt.Columns.Add(nome);
            dt.Columns.Add(administradora);

            foreach (var item in condominioList)
            {
                DataRow row = dt.NewRow();
                row[0] = item.NomeCondominio;
                row[1] = item.Administradora.NomeAdministradora;

                dt.Rows.Add(row);


            }





            dtGrid.ItemsSource = dt.DefaultView;
        }

        private async void Emails_Click(object sender, RoutedEventArgs e)
        {
            dt = new DataTable();
            EnviarForm.Visibility = Visibility.Collapsed;

            dtGrid.Visibility = Visibility.Visible;
            UsuarioService usuarioService = new UsuarioService();
            List<Email> emailList = new List<Email>();
            emailList = await usuarioService.EmailListar();

            DataColumn de = new DataColumn("De", typeof(string));
            DataColumn para = new DataColumn("Para", typeof(string));
            DataColumn assunto = new DataColumn("Condomínio", typeof(string));
            DataColumn conteudo = new DataColumn("Conteúdo", typeof(string));

            dt.Columns.Add(de);
            dt.Columns.Add(para);
            dt.Columns.Add(assunto);
            dt.Columns.Add(conteudo);

            foreach (var item in emailList)
            {
                DataRow row = dt.NewRow();
                row[0] = item.De;
                row[1] = item.Para;
                row[2] = item.Assunto;
                row[3] = item.Conteudo;


                dt.Rows.Add(row);


            }
                                  

            dtGrid.ItemsSource = dt.DefaultView;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        
        }

        private async void Enviar_Click(object sender, RoutedEventArgs e)
        {
            dtGrid.ItemsSource = null;
            dtGrid.Visibility = Visibility.Collapsed;
            cmbDestinatario.Items.Clear();
            cmbRemetente.Items.Clear();

            EnviarForm.Visibility = Visibility.Visible;
            UsuarioService usuarioService = new UsuarioService();
            List<Usuario> usuarioList = new List<Usuario>();
            usuarioList = await usuarioService.UsuarioListar();

            foreach (var item in usuarioList)
            {
                cmbDestinatario.Items.Add(item.Email);
                cmbRemetente.Items.Add(item.Email);
            }
        }

        private async void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            //Cria um novo objeto Email e seta suas propriedades com o que foi colocado pelo usuário no formulário
            Email email = new Email();
            email.De = cmbRemetente.Text;
            email.Para = cmbDestinatario.Text;
            email.Assunto = txtAssunto.Text;
            email.Conteudo = txtConteudo.Text;

            //Cria um novo service e usa PUT para alterar um e-mail enviado
            UsuarioService usuarioService = new UsuarioService();

            //Se foi feito o PUT com sucesso, dá um alert com sucesso, caso contrário, com erro.
            if (await usuarioService.Alterar(email) == null)
                MessageBox.Show("Ocorreu um erro, por favor, tente novamente.");
            else
                MessageBox.Show("Enviado com sucesso!");

            //Limpa os textbox's
            txtAssunto.Text = null;
            txtConteudo.Text = null;
        }
    }
}
