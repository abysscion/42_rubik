####################################################################################
# WINDOWS BUILD GUIDE:

# 1) Get nuget.exe command line
wget https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile nuget.exe

# 2) Download the C# Roslyn compiler
.\nuget.exe install Microsoft.Net.Compilers

# 3) Compile
.\Microsoft.Net.Compilers.%YOUR VERSION%\tools\csc.exe *.cs

# 4) Run it
.\Rubik.exe "F R U B L D"
####################################################################################
# MAC OS BUILD GUIDE:

# 1) Get from homebrew or install from (https://www.mono-project.com/)
brew install mono

# 2) Compile
mcs *.cs

# 3) Run
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
