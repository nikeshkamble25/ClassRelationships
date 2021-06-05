using System;
using System.Collections.Generic;

namespace ClassRelationships
{
    class Program
    {
        static void Main(string[] args)
        {
            BroadbandProvider broadbandProvider = new BroadbandProvider();
            broadbandProvider.Name = "ION";
            broadbandProvider.ComponyRatings = 4;

            Customer customer = new Customer(broadbandProvider);
            customer.CustName = "Nikesh Kamble";
            WifiConnection wifiConnection = new WifiConnection();
            wifiConnection.ConnectionId = 101;
            customer.AssignWifiConnection(wifiConnection);

            //Customer Raise an issue
            customer.IssueActive = true;

            broadbandProvider.ResolveIssue(customer);

            Console.ReadLine();
        }
    }
    class Customer
    {
        private bool _issueActive = false;
        private readonly BroadbandProvider broadbandProvider;        
        public Customer(BroadbandProvider broadbandProvider)
        {
            this.broadbandProvider = broadbandProvider;
        }
        public string CustName { get; set; }
        public string CustAddress { get; set; }
        public bool IssueActive {
            get
            {
                return _issueActive;
            }
            set     
            {
                if (value)
                {
                    this.broadbandProvider.ComponyRatings--;
                }
                else
                {
                    this.broadbandProvider.ComponyRatings++;
                }
                Console.WriteLine(this.broadbandProvider.ComponyRatings + " Ratings");
                _issueActive = value;
            }
        }
        public void AssignWifiConnection(WifiConnection wifiConnection)
        {
            wifiConnection.AddConnectionToCustomer(this);            
        }
        public void RaiseAnIssue()
        {
            this.IssueActive = true;
            
        }
    }
    //1.* CorporateCustomer is a type of Customer
    //This is simple example of Inheritance
    class CorporateCustomer : Customer
    {
        public CorporateCustomer(BroadbandProvider broadbandProvider) : base(broadbandProvider)
        {
        }

        public void AddMultipleConnection()
        {
            Console.Write($"Added muthiple connections for customer {this.CustName}");
        }
    }
    //2.* Bi-directional Association with Wifi Connection and Customer
    //Where both can be independatly co exists
    //Customer can exists without wifi connection he can use LAN connection instead
    //Wifi connection should not really dependent on customer this can be assigned to a government or local office    
    class WifiConnection
    {
        public int ConnectionId { get; set; }
        public void AddConnectionToCustomer(Customer customer)
        {
            Console.WriteLine($"Connection has been added for customer {customer.CustName} with customer id {ConnectionId}");
        }
        public void AddConnectionToGovernment()
        {
        }
        public void AddConnectionToLocalOffice()
        {
        }
    }

    class BroadbandProvider
    {
        public string Name { get; set; }
        public string PANId { get; set; }
        //3*. Agreegation with Wifi Connection
        //Broadband company could have mutiple connection
        public List<WifiConnection> connections { get; set; }
        public int ComponyRatings { get; set; }
        public void ResolveIssue(Customer customer) 
        {
            customer.IssueActive = false;
        }
    }
}
