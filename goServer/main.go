package main

import (
	"fmt"
	"goserver/app/server"
	"goserver/config"
)

func main() {
	fmt.Println("Hi I m server")
	server.StartServer(config.TCP_PORT)
}
