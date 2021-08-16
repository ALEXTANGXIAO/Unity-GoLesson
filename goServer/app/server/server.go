package server

import (
	"net"
	"runtime"

	"github.com/wonderivan/logger"
)

type Server struct {
	Port string
}

func NewServer(port string) *Server {
	server := &Server{Port: port}
	return server
}

func StartServer(port string) error {
	server := NewServer(port)
	logger.Info("Server Start", server)
	tcpAddr, err := net.ResolveTCPAddr("tcp4", server.Port)
	if err != nil {
		logger.Error(err)
		return err
	}
	listener, err := net.ListenTCP("tcp4", tcpAddr)

	var connid uint32 = 1000
	for {
		conn, err := listener.Accept()
		if err != nil {
			logger.Error(err)
			continue
		}
		connid++
		go handleClient(conn, connid)
	}
}

func handleClient(conn net.Conn, connid uint32) {
	defer runtime.Goexit()
	defer conn.Close()
	logger.Info(connid, conn)
	client, err := NewClient(conn, connid)
	if err != nil {
		logger.Error(err)
		return
	}
	buff := make([]byte, 1024)
	for {
		cnt, err := conn.Read(buff)
		if err != nil {
			logger.Error(err)
			break
		}
		//todo
		logger.Debug(client, cnt, buff)
	}
}

func CheckError(err error) error {
	if err != nil {
		return err
	}
	return nil
}
