cd externals/macos/AppCenter.framework
cp Versions/A/AppCenter AppCenter
ln -s Versions/A/Headers Headers
ln -s Versions/A/Modules Modules
ln -s Versions/A/PrivateHeaders PrivateHeaders
ln -s Versions/A/Resources Resources
cd Versions
ln -s A Current

cd .. ; cd ..
cd  AppCenterAnalytics.framework
cp Versions/A/AppCenterAnalytics AppCenterAnalytics
ln -s Versions/A/Headers Headers
ln -s Versions/A/Modules Modules
ln -s Versions/A/Resources Resources
cd Versions
ln -s A Current

cd .. ; cd ..
cd  AppCenterCrashes.framework
cp Versions/A/AppCenterCrashes AppCenterCrashes
ln -s Versions/A/Headers Headers
ln -s Versions/A/Modules Modules
ln -s Versions/A/Resources Resources
cd Versions
ln -s A Current
