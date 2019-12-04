﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Classes;

namespace Casus___Containervervoer
{
    public partial class Form1 : Form
    {
        private readonly List<Container> _containers;
        private Ship _ship;
        private Algorithm alg;
        
        public Form1()
        {
            _containers = new List<Container>();
            
            InitializeComponent();
        }

        private void btnAddContainer_Click(object sender, System.EventArgs e)
        {
            int weight = (int)numContainerWeight.Value;
            Container.Categories cat;

            if (checkCooled.Checked && checkValuable.Checked) { cat = Casus___Containervervoer.Container.Categories.ValuableCooled; }
            else if (checkValuable.Checked) { cat = Casus___Containervervoer.Container.Categories.Valuable; }
            else if (checkCooled.Checked ){ cat = Casus___Containervervoer.Container.Categories.Cooled; }
            else { cat = Casus___Containervervoer.Container.Categories.Normal; }

            var container = new Container(cat, weight);

            if (!container.CheckWeightContainer((int)numContainerWeight.Value))
            {
                _containers.Add(container);

                listContainers.Items.Add(container);

                rtbLog.ForeColor = Color.Green;
                rtbLog.Text = $"Container ({container}) has been added succesfully";

                lblContainerTotal.Text = listContainers.Items.Count.ToString();

                int totalWeight = TotalWeight(_containers);
                if (totalWeight >= _ship.MinWeight)
                {
                    if (!container.CheckTotalWeightContainer(_ship.MinWeight, _ship.MaxWeight, _containers))
                    {
                        rtbLog.ForeColor = Color.Red;
                        rtbLog.Text = $"The total weight of the containers exceeds the weight of the ship. Please remove a container to continue";
                    }
                }
                
                lblContainerWeight.Text = $"{totalWeight} tons";
            }
            else
            {
                rtbLog.ForeColor = Color.Red;
                rtbLog.Text = $"Sorry but the weight of the container you're trying to add is not correct. The weight needs to be between 4 and 30 tons. The current weight of the container is {weight} tons. ";
            }
            File.AppendAllText("log.txt", $"[{DateTime.Now.ToString()}]: {rtbLog.Text}\n");

        }

        private void btnSetShipWeight_Click(object sender, System.EventArgs e)
        {
            if (numLength.Value <= 0 || numWidth.Value <= 0)
            {
                rtbLog.ForeColor = Color.Red;
                rtbLog.Text =
                    $"Sorry, but the ship must be at least 1 wide and 1 long. Currently you have set the following values:\n" +
                    $"- Length:\t\t {numLength.Value} \n" +
                    $"- Width:\t\t {numWidth.Value}";
                File.AppendAllText("log.txt", $"[{DateTime.Now.ToString()}]: {rtbLog.Text}\n");
                return;
            }
            _ship = new Ship((int)numLength.Value, (int)numWidth.Value);
            btnSetShipWeight.Enabled = false;
            btnAddContainer.Enabled = true;
            rtbLog.ForeColor = Color.Green;
            rtbLog.Text = $"The Values of the ship has been set \n" +
                          $"- Ship Length:\t{_ship.Lenght}\n" +
                          $"- Ship Width:\t{_ship.Width}\n" +
                          $"- Max Weight:\t{_ship.MaxWeight}\n" +
                          $"- Min Weight:\t{_ship.MinWeight}";
            File.AppendAllText("log.txt", $"[{DateTime.Now.ToString()}]: {rtbLog.Text}\n");
            lblShipLength.Text = _ship.Lenght.ToString();
            lblShipWidth.Text = _ship.Width.ToString();
            lblShipMaxWeight.Text = _ship.MaxWeight.ToString();
            lblShipMinWeight.Text = _ship.MinWeight.ToString();
        }

        private void btnContainerDelete_Click(object sender, EventArgs e)
        {
            int index = listContainers.SelectedIndex;

            if (index == -1)
            {
                rtbLog.ForeColor = Color.Red;
                rtbLog.Text = "No container has been selected";
                File.AppendAllText("log.txt", $"[{DateTime.Now.ToString()}]: {rtbLog.Text}\n");
                return;
            }

            rtbLog.ForeColor = Color.Green;
            rtbLog.Text = $"Container ({listContainers.SelectedItem}) has been removed succesfully";
            File.AppendAllText("log.txt", $"[{DateTime.Now.ToString()}]: {rtbLog.Text}\n");
            listContainers.Items.RemoveAt(index);
            _containers.RemoveAt(index);
            lblContainerTotal.Text = listContainers.Items.Count.ToString();

            int totalWeight = TotalWeight(_containers);

            lblContainerWeight.Text = $"{totalWeight} tons";
        }

        private void btnContainerDeleteAll_Click(object sender, EventArgs e)
        {
            listContainers.Items.Clear();
            _containers.Clear();
            rtbLog.ForeColor = Color.Green;
            rtbLog.Text = $"All containers have been removed";
            File.AppendAllText("log.txt", $"[{DateTime.Now.ToString()}]: {rtbLog.Text}\n");
            lblContainerTotal.Text = "No containers added";

        }

        private void btnCalculation_Click(object sender, EventArgs e)
        {
            int totalWeight = TotalWeight(_containers);
            if (totalWeight < _ship.MinWeight)
            {
                rtbLog.ForeColor = Color.Red;
                rtbLog.Text =
                    $"The total weight of the containers is lower than the minimum threshold, add more containers to the ship: \n" +
                    $"- Total Weight:\t {totalWeight}";
            }
            else
            {
                alg = new Algorithm();
                alg.SortContainerByCategory(_containers);
                alg.SortContainerLists();
                alg.CreateRows(_ship.Lenght, _ship.Width);
                //for (int i = 0; i < _ship.Lenght; i++)
                //{
                //    if (!alg.AddCooledContainersToStack(i, alg.FindLowestStack(i)))
                //        continue;
                //    alg.AddCooledContainersToStack(i, alg.FindLowestStack(i));
                //}
                if (alg.AddCooledContainersToStack(0, alg.FindLowestStack(0)))
                {
                    alg.AddCooledContainersToStack(0, alg.FindLowestStack(0));
                }
                else
                {
                    rtbLog.ForeColor = Color.Red;
                    rtbLog.Text = "Sorry but there are too many cooled containers";
                    File.AppendAllText("log.txt", $"[{DateTime.Now.ToString()}]: {rtbLog.Text}\n");
                }

                rtbLog.Text = "Calculation has been made";
                File.AppendAllText("log.txt", $"[{DateTime.Now.ToString()}]: {rtbLog.Text}\n");


            }
            //System.Diagnostics.Process.Start("https://i872272core.venus.fhict.nl/ContainerVisualizer/index.html");
        }

        private int TotalWeight(List<Container> containers)
        {
            int totalWeight = 0;
            foreach (var item in _containers)
            {
                totalWeight += item.Weight;
            }

            return totalWeight;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            File.Open("log.txt", FileMode.Open);
        }
    }
}
