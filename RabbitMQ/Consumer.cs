using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RabbitMQ
{
    public partial class Consumer : Form
    {
        private IConnection _rabbitMqConnection;
        private IModel _myChannel;
        string _hostName = "";
        string _customport = "";
        string _userName = "";
        string _password = "";
        string _virtualHost = "";
        string _routingKey = "";
        string _queueName = "";
        string _exchangeName = "";
        string _producerConnectionName = "";
        public Consumer()
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
            _routingKey = System.Configuration.ConfigurationManager.AppSettings["routingKey"];
            _queueName = System.Configuration.ConfigurationManager.AppSettings["queueName"];
            _exchangeName = System.Configuration.ConfigurationManager.AppSettings["exchangeName"];
            _producerConnectionName = System.Configuration.ConfigurationManager.AppSettings["producerConnectionName"];
        }
        private Task ChannelConsumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body.ToArray());
            //lstSmsMessages.Invoke((MethodInvoker)(() => lstSmsMessages.Items.Add(message)));
            this.BeginInvoke(new MethodInvoker(delegate
            {
                this.txtBxDataShow.Text = this.txtBxDataShow.Text + Environment.NewLine + message;
            }));
            _myChannel.BasicAck(e.DeliveryTag, false);

            return Task.FromResult(true);
        }
        private void btnGET_Click(object sender, EventArgs e)
        {
            _myChannel = _rabbitMqConnection.CreateModel();
            //var data = _smsChannel.BasicGet(_queueName, true);

            _myChannel.BasicQos(0, 1, false);
            var smsChannelConsumer = new AsyncEventingBasicConsumer(_myChannel);
            smsChannelConsumer.Received += ChannelConsumer_Received; ;
            _myChannel.BasicConsume(_queueName, true, smsChannelConsumer);


            //string hostName = System.Configuration.ConfigurationManager.AppSettings["hostName"];
            //var factory = new ConnectionFactory() { HostName = hostName };
            //using (var connection = factory.CreateConnection())
            //{
            //    using (var channel = connection.CreateModel())
            //    {
            //        // Declare the queue here because we might start the consumer before the publisher 
            //        // so the queue should exist before we try to consume messages from it.
            //        channel.QueueDeclare(queue: "Test Queue",
            //                             durable: true,
            //                             exclusive: false,
            //                             autoDelete: false,
            //                             arguments: null);

            //        var consumer = new EventingBasicConsumer(channel);

            //        // Register a consumer to listen to a specific queue. 
            //        channel.BasicConsume(queue: "Test Queue",
            //                             //autoAck: true,
            //                             consumer: consumer);

            //        consumer.Received += (model, ea) =>
            //        {
            //            var body = ea.Body;
            //             var msg = Encoding.UTF8.GetString(ea.Body.ToArray());
            //            this.txtBxDataShow.Text= this.txtBxDataShow.Text+Environment.NewLine+msg;
            //            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            //        };
            //    }
            //}
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
