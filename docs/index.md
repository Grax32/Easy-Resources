
	## Fetch from any assembly by specifying a type that is defined in the target assembly.
	string resource = EasyResources.GetStringResource<UnitTest1>("/Resources/textfile.txt"); 
	byte[] resourceBytes = EasyResources.GetResource<UnitTest1>("/Resources/Image.png");
	
	## Fetch from the executing assembly 
	string resource = assembly.GetStringResource("/Resources/textfile.txt");
	byte[] resourceBytes = assembly.GetResource("/Resources/Image.png");
