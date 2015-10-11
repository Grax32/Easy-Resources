# Easy-Resources
Fetch embedded resources easily


	string resource = EasyResources.GetStringResource<UnitTest1>("/Resources/textfile.txt"); 
	byte[] resourceBytes = EasyResources.GetResource<UnitTest1>("/Resources/Image.png");
	string resource = assembly.GetStringResource("/Resources/textfile.txt");
	byte[] resourceBytes = assembly.GetResource("/Resources/Image.png");