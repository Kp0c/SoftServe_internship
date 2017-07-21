#include "DataSource.h"
#include <iostream>
#include <set>

bool TryInsert(std::set<Vertex, VertexComparator>& vertexes, Connection connection)
{
    bool isInserted = false;

    if (vertexes.find(connection.to) != vertexes.end() && vertexes.find(connection.from) == vertexes.end())
    {
        isInserted = true;
        vertexes.insert(connection.from);
    }
    if (vertexes.find(connection.from) != vertexes.end() && vertexes.find(connection.to) == vertexes.end())
    {
        isInserted = true;
        vertexes.insert(connection.to);
    }

    return isInserted;
}

std::set<Vertex, VertexComparator> TraverseConnectedData(std::vector<Connection> connections, Vertex vertex)
{
    std::set<Vertex, VertexComparator> connectedVertexes{ vertex };

    bool isConnected = true;
    while (isConnected)
    {
        isConnected = false;

        for (size_t i = 0; i < connections.size(); ++i)
        {
            if (TryInsert(connectedVertexes, connections[i]))
            {
                isConnected = true;
            }
            else
            {
                isConnected = false;
            }
        }
    }
    
    return connectedVertexes;
}

int main()
{
    std::vector<Connection> cons = GetConnections();
    std::vector<Vertex> vertexes = GetVertexes();

    while (vertexes.size() > 0)
    {
        std::set<Vertex, VertexComparator> group = TraverseConnectedData(cons, vertexes[0]);

        std::cout << "Cycle: ";
        Vertex max = { INT_MIN, INT_MIN };
        for (Vertex vert : group)
        {
            std::cout << vert.number << " ";
            if (vert.weight > max.weight)
            {
                max = vert;
            }
        }
        std::cout << "Max number: " << max.number << " with weight " << max.weight << std::endl;

        //remove used connections
        for (auto i = cons.begin(); i != cons.end();)
        {
            if (group.find(i->from) != group.end() || group.find(i->to) != group.end())
            {
                i = cons.erase(i);
            }
            else
            {
                ++i;
            }
        }

        //remove used vertexes
        for (auto i = vertexes.begin(); i != vertexes.end();)
        {
            if (group.find(*i) != group.end())
            {
                i = vertexes.erase(i);
            }
            else
            {
                ++i;
            }
        }

        std::cout << std::endl;
    }
}
