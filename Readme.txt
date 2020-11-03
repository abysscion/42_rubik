####################################################################################
WINDOWS BUILD GUIDE:

# Get nuget.exe command line
wget https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile nuget.exe

# Download the C# Roslyn compiler
.\nuget.exe install Microsoft.Net.Compilers

# Compile
.\Microsoft.Net.Compilers.%YOUR VERSION%\tools\csc.exe *.cs

# Run it
.\Rubik.exe "F R U B L D"
####################################################################################
MAC OS BUILD GUIDE:

# Get from homebrew or install from (https://www.mono-project.com/)
brew install mono

# Compile
mcs *.cs

# Run
mono Rubik.exe "F R U B L D"

####################################################################################

Launch with command sequence as arguments to get instant solve.

Launch without arguments to get into interactive move.

Rotations (use shift + key to get counterclockwise rotation):
T - up clockwise
M - down clockwise
G - left clockwise
J - right clockwise
Y - front clockwise
N - back clockwise

Esc - exit
Q - random shuffle
Z - solve
P - performance test
