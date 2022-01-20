import sys
from OpenGL.GL import *
from OpenGL.GLU import *
from OpenGL.GLUT import *
import pygame
from pygame.locals import *
import pywavefront
from pywavefront import visualization
import numpy as np
import time

obj = pywavefront.Wavefront('objekti/bird.obj')

width = 1000
height = 800

t = 0.0

km1 = np.array(
    [[-1,3,-3,1],
    [3,-6,3,0],
    [-3,0,3,0],
    [1,4,1,0]]
)

km2 = np.array(
    [[-1,3,-3,1],
    [2,-4,2,0],
    [-1,0,1,0]]
)

kontrolne_tocke2 = np.array(
    [[0,0,-15],
    [15,0,-25],
    [25,0,-25],
    [30,0,-35],
    [50,0,-40],
    [55,0,-50],
    [65,0,-65],
    [75,0,-65]]
)

kontrolne_tocke = np.array(
    [[0,0,0],
    [0,10,5],
    [10,10,10],
    [10,0,15],
    [0,0,20],
    [0,10,25],
    [10,10,30],
    [10,0,35],
    [0,0,40],
    [0,10,45],
    [10,10,50],
    [10,0,55]]
)

def T(t):
    return np.array([np.power(t,3), np.power(t,2), np.power(t,1), 1])

def T_der(t):
    return np.array([np.power(t, 2), np.power(t, 1), 1])

def P(t, i):

    R = np.array([kontrolne_tocke[i-1],kontrolne_tocke[i],kontrolne_tocke[i+1],kontrolne_tocke[i+2]])

    return np.matmul(np.matmul((1.0/6)*T(t), km1), R)

def tangent(t, i):

    R = np.array([kontrolne_tocke[i-1],kontrolne_tocke[i],kontrolne_tocke[i+1],kontrolne_tocke[i+2]])

    return np.matmul(np.matmul((1.0/2)*T_der(t), km2), R)

points = []
tangents = []

def calculate_transations():
    for i in range(1, len(kontrolne_tocke) - 2):
        for t in np.linspace(0, 1, 100):
            p = P(t, i)
            points.append(p)
            tang = tangent(t,i)
            tangents.append(tang)

class Object:
    vs = []
    fs = []

    def __init__(self, filename):
        file = open(filename, "r")
        for line in file.readlines():
            if line[0] == "v":
                string_vertices = line[1::].strip().split(" ")
                vertex = (float(string_vertices[0]), float(string_vertices[1]), float(string_vertices[2]))
                self.vs.append(vertex)
            if line[0] == "f":
                string_faces = line[1::].strip().split(" ")
                face = (int(string_faces[0]) -1, int(string_faces[1])-1, int(string_faces[2])-1)
                self.fs.append(face)

    def draw(self):
        glColor(0,0,0)
        for face in self.fs:
            glBegin(GL_LINE_LOOP)
            for vertex in face:
                v = self.vs[vertex]
                glVertex3fv(self.vs[vertex])
            glEnd()

object = Object("objekti/bird.obj")

i = 1

def iterate():
    glMatrixMode(GL_PROJECTION)
    glLoadIdentity()
    glMatrixMode (GL_MODELVIEW)
    glLoadIdentity()


def draw_curve():
    glColor(0,0,0)
    glBegin(GL_LINE_STRIP)
    for p in points:
        glVertex3fv(p)
    glEnd()

ind = 0


def draw_tangent(ind):
    glColor(0, 0, 0)
    glBegin(GL_LINE_STRIP)
    glVertex3fv(points[ind])
    glVertex3fv(points[ind] + tangents[ind])
    glEnd()


def showScreen():
    global ind
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT)
    glLoadIdentity()
    gluPerspective(45, (width / height), 0.1, 100.0)

    pos = np.array([0,0,-20])
    target = np.array([0,0,0])
    up = np.array([0, 1, 0])
    gluLookAt(pos[0],pos[1],pos[2], target[0],target[1],target[2], up[0],up[1],up[2])

    glClearColor(1, 1, 1, 1)
    draw_curve()

    draw_tangent(ind)

    glTranslatef(points[ind][0],points[ind][1],points[ind][2])
    cos = 0
    s = np.array([0,0,1])
    e = tangents[ind]

    os = np.array([s[1]*e[2] - e[1]*s[2], -(s[0]*e[2] - e[0]*s[2]), s[0]*e[1] - s[1]*e[0]])
    cos = (np.matmul(tangents[ind], os.T)) / (
                np.linalg.norm(tangents[ind]) * np.linalg.norm(os))
    angle = np.degrees(np.arccos(cos))
    glRotate(angle, os[0],os[1],os[2])



    ind = ind + 1
    glScalef(4,4,4)
    visualization.draw(obj)
    time.sleep(0.01)
    #object.draw()

    glutSwapBuffers()



def main():
    object = Object("objekti/teddy.obj")
    calculate_transations()

    pygame.init()
    display = (width, height)

    pygame.display.set_mode(display, DOUBLEBUF|OPENGL)

    gluPerspective(45, (width / height), 0.1, 100.0)
    glTranslate(0.0, 0.0, -20)
    glRotate(30.0, 30.0, 30.0, 0.0)

    while True:
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                pygame.quit()
                quit()

        glClearColor(1, 1, 1, 1)
        glClear(GL_COLOR_BUFFER_BIT|GL_DEPTH_BUFFER_BIT)

        #visualization.draw(obj)
        #object.draw()

        pygame.display.flip()
        pygame.time.wait(10)

def main2():
    calculate_transations()
    glutInit()
    glutInitDisplayMode(GLUT_RGBA)
    glutInitWindowSize(500, 500)
    glutInitWindowPosition(0, 0)
    wind = glutCreateWindow("OpenGL Coding Practice")
    glutDisplayFunc(showScreen)
    glutIdleFunc(showScreen)
    glutMainLoop()

main2()


