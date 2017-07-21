#pragma once
#include <memory>
#include <vector>

struct Vertex
{
    int number;
    int weight;
};

class VertexComparator
{
public:
    bool operator()(Vertex v1, Vertex v2) const
    {
        return v1.number < v2.number;
    }
};

struct Connection
{
    Vertex from;
    Vertex to;
};

static const std::vector<Vertex> vertexes
{
    { 1, 9 },
    { 2, 3 },
    { 3, 5 },
    { 4, 9 },
    { 5, 5 },
    { 6, 8 },
    { 7, 2 },
    { 8, 11 },
    { 9, 11 },
    { 10, 7 },
    { 11, 5 },
    { 1,  1  }
};

static const std::vector<Connection> connections
{
    { vertexes[2],  vertexes[0] },
    { vertexes[0],  vertexes[9] },
    { vertexes[0],  vertexes[1] },
    { vertexes[5],  vertexes[6] },
    { vertexes[3],  vertexes[8] },
    { vertexes[9],  vertexes[7] }
};

std::vector<Connection> GetConnections()
{
    return std::vector<Connection>(connections);
}

std::vector<Vertex> GetVertexes()
{
    return std::vector<Vertex>(vertexes);
}