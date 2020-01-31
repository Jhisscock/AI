# search.py
# ---------
# Licensing Information:  You are free to use or extend these projects for
# educational purposes provided that (1) you do not distribute or publish
# solutions, (2) you retain this notice, and (3) you provide clear
# attribution to UC Berkeley, including a link to http://ai.berkeley.edu.
# 
# Attribution Information: The Pacman AI projects were developed at UC Berkeley.
# The core projects and autograders were primarily created by John DeNero
# (denero@cs.berkeley.edu) and Dan Klein (klein@cs.berkeley.edu).
# Student side autograding was added by Brad Miller, Nick Hay, and
# Pieter Abbeel (pabbeel@cs.berkeley.edu).


"""
In search.py, you will implement generic search algorithms which are called by
Pacman agents (in searchAgents.py).
"""

import util

class SearchProblem:
    """
    This class outlines the structure of a search problem, but doesn't implement
    any of the methods (in object-oriented terminology: an abstract class).

    You do not need to change anything in this class, ever.
    """

    def getStartState(self):
        """
        Returns the start state for the search problem.
        """
        util.raiseNotDefined()

    def isGoalState(self, state):
        """
          state: Search state

        Returns True if and only if the state is a valid goal state.
        """
        util.raiseNotDefined()

    def getSuccessors(self, state):
        """
          state: Search state

        For a given state, this should return a list of triples, (successor,
        action, stepCost), where 'successor' is a successor to the current
        state, 'action' is the action required to get there, and 'stepCost' is
        the incremental cost of expanding to that successor.
        """
        util.raiseNotDefined()

    def getCostOfActions(self, actions):
        """
         actions: A list of actions to take

        This method returns the total cost of a particular sequence of actions.
        The sequence must be composed of legal moves.
        """
        util.raiseNotDefined()


def tinyMazeSearch(problem):
    """
    Returns a sequence of moves that solves tinyMaze.  For any other maze, the
    sequence of moves will be incorrect, so only use this for tinyMaze.
    """
    from game import Directions
    s = Directions.SOUTH
    w = Directions.WEST
    return  [s, s, w, s, w, w, s, w]

def depthFirstSearch(problem):
    """
    Search the deepest nodes in the search tree first.

    Your search algorithm needs to return a list of actions that reaches the
    goal. Make sure to implement a graph search algorithm.

    To get started, you might want to try some of these simple commands to
    understand the search problem that is being passed in:

    print "Start:", problem.getStartState()
    print "Is the start a goal?", problem.isGoalState(problem.getStartState())
    print "Start's successors:", problem.getSuccessors(problem.getStartState())
    """
    "*** YOUR CODE HERE ***"
    print "Start:", problem.getStartState()
    print "Is the start a goal?", problem.isGoalState(problem.getStartState())
    print "Start's successors:", problem.getSuccessors(problem.getStartState())

    #Initializing graph#
    s = util.Stack()
    visited = []
    path = []
    
    #Check to see if starting node is our goal#
    if problem.isGoalState(problem.getStartState()):
        return []

    #Begin traversing tree#
    startNode = (problem.getStartState(), [])
    s.push(startNode)
    while not s.isEmpty():
        #Add and check current node, return path if current node is the goal state#
        tmp = s.pop()
        currNode = tmp[0]
        path = tmp[1]
        if path not in visited:
            visited.append(currNode)
            if problem.isGoalState(currNode):
                return path
            
            #Traverse through the tree pushing the current new node and the new direction#
            for nodes in list(problem.getSuccessors(currNode)):
                if nodes[0] not in visited:
                    s.push((nodes[0],path + [nodes[1]]))
                    
    util.raiseNotDefined()
def breadthFirstSearch(problem):
    """Search the shallowest nodes in the search tree first."""
    "*** YOUR CODE HERE ***"
    print "Start:", problem.getStartState()
    print "Is the start a goal?", problem.isGoalState(problem.getStartState())
    print "Start's successors:", problem.getSuccessors(problem.getStartState())

    #Initializing graph#
    q = util.Queue()
    visited = []
    path = []
    
    #Check to see if starting node is our goal#
    if problem.isGoalState(problem.getStartState()):
        return []

    #Begin traversing tree#
    startNode = (problem.getStartState(), [])
    q.push(startNode)
    while not q.isEmpty():
        #Add and check current node, return path if current node is the goal state#
        tmp = q.pop()
        currNode = tmp[0]
        path = tmp[1]
        if path not in visited:
            visited.append(currNode)
            if problem.isGoalState(currNode):
                return path
            
            #Traverse through the tree pushing the current new node and the new direction#
            for nodes in list(problem.getSuccessors(currNode)):
                if nodes[0] not in visited:
                    q.push((nodes[0],path + [nodes[1]]))
                    
    util.raiseNotDefined()

def uniformCostSearch(problem):
    """Search the node of least total cost first."""
    "*** YOUR CODE HERE ***"
    print "Start:", problem.getStartState()
    print "Is the start a goal?", problem.isGoalState(problem.getStartState())
    print "Start's successors:", problem.getSuccessors(problem.getStartState())
    
    #Initializing graph#
    q = util.PriorityQueue()
    visited = []
    path = []
    
    #Check to see if starting node is our goal#
    if problem.isGoalState(problem.getStartState()):
        return []

    #Begin traversing tree#
    q.push((problem.getStartState(), [], 0), 0)
    while not q.isEmpty():
        #Add and check current node, return path if current node is the goal state#
        tmp = q.pop()
        currNode = tmp[0]
        path = tmp[1]
        cost = tmp[2]
        print(currNode, path, cost)
        if path not in visited:
            visited.append(currNode)
            if problem.isGoalState(currNode):
                return path
            
            #Push the successors nodes into the PQueue adding the new path and cost every time#
            for nextNode, action, newCost in list(problem.getSuccessors(currNode)):
                if nextNode and nextNode not in visited:
                    q.push((nextNode, path + [action], cost + newCost),cost + newCost)
                    
    util.raiseNotDefined()

def nullHeuristic(state, problem=None):
    """
    A heuristic function estimates the cost from the current state to the nearest
    goal in the provided SearchProblem.  This heuristic is trivial.
    """
    return 0

def aStarSearch(problem, heuristic=nullHeuristic):
    """Search the node that has the lowest combined cost and heuristic first."""
    "*** YOUR CODE HERE ***"
    print "Start:", problem.getStartState()
    print "Is the start a goal?", problem.isGoalState(problem.getStartState())
    print "Start's successors:", problem.getSuccessors(problem.getStartState())
    
    #Initializing graph#
    q = util.PriorityQueue()
    visited = []
    path = []
    
    #Check to see if starting node is our goal#
    if problem.isGoalState(problem.getStartState()):
        return []

    #Begin traversing tree#
    q.push((problem.getStartState(), [], 0), 0)
    while not q.isEmpty():
        #Add and check current node, return path if current node is the goal state#
        tmp = q.pop()
        currNode = tmp[0]
        path = tmp[1]
        cost = tmp[2]
        print(currNode, path, cost)
        if path not in visited:
            visited.append(currNode)
            if problem.isGoalState(currNode):
                return path
            
            #Push the successors nodes into the PQueue adding the new path and cost every time#
            for nextNode, action, newCost in list(problem.getSuccessors(currNode)):
                if nextNode and nextNode not in visited:
                    q.push((nextNode, path + [action], cost + newCost),cost + newCost + heuristic(nextNode, problem))
                    
    util.raiseNotDefined()


# Abbreviations
bfs = breadthFirstSearch
dfs = depthFirstSearch
astar = aStarSearch
ucs = uniformCostSearch
