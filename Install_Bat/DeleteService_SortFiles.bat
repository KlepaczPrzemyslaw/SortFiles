ECHO "This script will stop SortFiles if is running and will delete it"

SC STOP "SortFiles"
SC DELETE "SortFiles"

ECHO "DONE!"
@pause