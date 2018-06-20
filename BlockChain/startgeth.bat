RD /S /Q %~dp0\TestChain\geth\chainData
RD /S /Q %~dp0\TestChain\geth\dapp
RD /S /Q %~dp0\TestChain\geth\nodes
del %~dp0\TestChain\geth\nodekey

geth.exe  --datadir=TestChain init genesis.json
geth.exe --mine --rpc --networkid=39318 --cache=2048 --maxpeers=0 --datadir=TestChain  --ipcpath "geth.ipc"  --rpccorsdomain "*" --rpcapi "eth,web3,personal,net,miner,admin,debug" --verbosity 0 console  