package server

import "net"

type Client struct {
	Conn  net.Conn
	Uniid uint32
}

func NewClient(conn net.Conn, uniid uint32) (*Client, error) {
	client := &Client{Conn: conn, Uniid: uniid}

	return client, nil
}
