from pyassimp import *
scene = load('objekti/teddy.obj')

assert len(scene.meshes)
mesh = scene.meshes[0]

assert len(mesh.vertices)
print(mesh.vertices[0])

# don't forget this one, or you will leak!
release(scene)