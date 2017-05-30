using System;

//BIAI Project 
    class Manager
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------- BIAI Project - Neural Network -----------------  ");
            Console.WriteLine("Iris setosa = 0 0 1,\nIris versicolor = 0 1 0,\nIris virginica = 1 0 0,\n ");
            Console.WriteLine("Data: sepal length, sepal width, petal length, petal width\n");
            Console.WriteLine("For example: 5.4, 3.4, 1.7, 0.2 ");
            Console.WriteLine("-------------------------------------------------------------------  ");
           
            double[][] data = null;
            Tools.fileReader(ref data, @"D:\DANE.txt"); //reading data from file
            double[][] dataForTest = null;
            double[][] dataForTraining = null;
            DataPrepare.makeTrainTest(data, out dataForTest, out dataForTraining);
            DataPrepare.normalize(dataForTest, new int[] { 0, 1, 2, 3 });
            DataPrepare.normalize(dataForTraining, new int[] { 0, 1, 2, 3 });

            Console.WriteLine("I'm creating neural network...");
            const int input = 4;
            const int hidden = 7;
            const int output = 3;
            NeuralNetwork nn = new NeuralNetwork(input, hidden, output);
            nn.initializeWeights(); //Initializing weights and bias to small random values

            //TRAINING

            //Settings for training
            int epochos = 2000;
            double learnRate = 0.05;
            double momentum = 0.01;
            double weightDecay = 0.0001;
            Console.WriteLine("Start training");
            DateTime startTime = DateTime.Now;
            nn.training(dataForTest, epochos, learnRate, momentum, weightDecay); //mean squared error < 0.020 stopping condition
            DateTime stopTime = DateTime.Now;
            TimeSpan roznica = stopTime - startTime;
            Console.WriteLine("Training time:" + roznica.TotalSeconds);
            Console.WriteLine("Training complete! :)");
            double[] weights = nn.getWeights();

            //Console.WriteLine("nn weights and bias values:");
             //Tools.showVector(weights, 10, 3, true);

            //Accurancy
            double accurancyTrain = nn.accuracy(dataForTest);
            Console.WriteLine("---------------------Accurancy on Neural Network-------------------  ");
            Console.WriteLine("\nAccuracy on training data = " + accurancyTrain.ToString("F2"));
            double accurancyTest = nn.accuracy(dataForTraining);
            Console.WriteLine("Accuracy on test data = " + accurancyTest.ToString("F2"));

             //TESTING BY USER
            double[] numbers = new double[4];
            string ask;
            Console.WriteLine("\n-------------------------------------------------------------------  ");
            Console.WriteLine("Would you like to test ? [Y/N]");
            ask = Console.ReadLine();
                while (ask.ToUpper() == "Y")
                     {
                       Console.WriteLine("Input 4 values");
                       for (int a = 0; a < 4; a++)
                           {
                               numbers[a] = Convert.ToDouble(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
                           }
                       Console.WriteLine(Tools.outputName(ref nn, DataPrepare.normalizeInput(numbers,data)));    
                       Console.WriteLine("Would u like to test one more time ? [Y/N]");
                       ask = Console.ReadLine();
                     }
            Console.ReadLine();
        } 
} 

    