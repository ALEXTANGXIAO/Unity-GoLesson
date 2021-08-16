package server

import (
	"net"
	"runtime"

	"github.com/wonderivan/logger"
)

var ClientMap = make(map[uint32]*Client)
var ClientList = []*Client{}

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
	listener, err := net.ListenTCP("tcp", tcpAddr)

	var connid uint32 = 1000
	for {
		conn, err := listener.Accept()
		if err != nil {
			logger.Error("accept failed, err:", err)
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

func AddClient(client *Client) error {
	_, handleClient := ClientMap[client.Uniid]
	if !handleClient {
		ClientMap[client.Uniid] = client
	} else {
		// return errors.New("repeat Uniid", client)
	}

	ClientList = append(ClientList, client)

	return nil
}

func RemoveClient(client *Client) error {
	_, handleClient := ClientMap[client.Uniid]
	if !handleClient {
		delete(ClientMap, client.Uniid)
	} else {
		// return errors.New("repeat Uniid", client)
	}

	ClientList = Remove(ClientList, client)

	return nil
}

func Remove(values []*Client, val *Client) []*Client {
	if len(values) <= 0 {
		return values
	}

	res := []*Client{}

	for i := 0; i < len(values); i++ {
		if values[i] == val {
			continue
		}
		v := values[i]
		res = append(res, v)
	}
	return res
}

func CheckError(err error) error {
	if err != nil {
		return err
	}
	return nil
}
