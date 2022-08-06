using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RabbitMQProducer
{
    public partial class Producer : Form
    {
        private IConnection _rabbitMqConnection;
        private IModel _emailChannel;
        private IModel _smsChannel;
        string _hostName = "";
        string _customport = "";
        string _userName = "";
        string _password = "";
        string _virtualHost = "";
        string _routingKey = "";
        string _queueName = "";
        string _exchangeName = "";
        string _producerConnectionName = "";
        public Producer()
        {
            InitializeComponent();
            Init();
        }
        void Init()
        {
            _hostName = System.Configuration.ConfigurationManager.AppSettings["hostName"];
            _customport = System.Configuration.ConfigurationManager.AppSettings["customport"];
            _userName = System.Configuration.ConfigurationManager.AppSettings["userName"];
            _password = System.Configuration.ConfigurationManager.AppSettings["password"];
            _virtualHost = System.Configuration.ConfigurationManager.AppSettings["virtualHost"];
            _routingKey= System.Configuration.ConfigurationManager.AppSettings["routingKey"];
            _queueName = System.Configuration.ConfigurationManager.AppSettings["queueName"];
            _exchangeName = System.Configuration.ConfigurationManager.AppSettings["exchangeName"];
            _producerConnectionName = System.Configuration.ConfigurationManager.AppSettings["producerConnectionName"];
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Plese Provide some data");
                    return;
                }

                //var channel = connection.CreateModel();
                //channel.ConfirmSelect();


                using (var channel = _rabbitMqConnection.CreateModel())
                {
                    channel.ConfirmSelect();
                    var message = textBox1.Text;//.Text;

                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2;
                    //channel.BasicPublish("desktopApp", "rabbitmqDesktopQ", properties, Encoding.UTF8.GetBytes(message));

                    channel.BasicPublish(exchange: _exchangeName, routingKey: _routingKey, 
                                         basicProperties: properties, Encoding.UTF8.GetBytes(message));
                  
                    MessageBox.Show("Data Published");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _hostName,
                    Port = string.IsNullOrEmpty(_customport) ? Protocols.DefaultProtocol.DefaultPort : Int32.Parse(_customport),
                    UserName = _userName,
                    Password = _password,
                    VirtualHost = _virtualHost,
                    ContinuationTimeout = new TimeSpan(10, 0, 0, 0)
                };
                factory.AutomaticRecoveryEnabled = true;
                factory.DispatchConsumersAsync = true;
                _rabbitMqConnection = factory.CreateConnection(_producerConnectionName);


                //Creating Exchange
                using (var channel = _rabbitMqConnection.CreateModel())
                {
                    channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct, true, false);
                }

                //Queue
                using (var channel = _rabbitMqConnection.CreateModel())
                {
                    channel.QueueDeclare(_queueName, true, false, false);
                    //channel.QueueDeclare("B", true, false, false);
                }

                //Bind Queue with Exchange
                using (var channel = _rabbitMqConnection.CreateModel())
                {
                    channel.QueueBind(_queueName, _exchangeName, _routingKey);
                    //channel.QueueBind("B", "desktopApp1", "dataTwo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
