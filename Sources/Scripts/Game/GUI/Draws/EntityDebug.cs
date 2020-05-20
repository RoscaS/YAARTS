using System.Linq;
using Game.Entities;
using UnityEngine;

public static class EntityDebug
{
    public static void DrawLineOfSight(Entity entity) {
        var color = entity.Awareness.aggro.Any() ? Color.yellow : Color.green;
        DrawingTools.Circle(entity.Position, entity.Awareness.ligneOfSight, color);
    }

    public static void DrawRange(Entity entity) {
        var color = (entity.Interaction.Target && entity.Interaction.TargetInRange())
                ? Color.red
                : Color.yellow;

        DrawingTools.Circle(entity.Position, entity.Interaction.Range, color);
    }

    public static void DrawAggroList(Entity entity) {
        var aggro = entity.Awareness.aggro;
        var closest = entity.Awareness.closest;

        if (aggro.Any()) {
            foreach (var target in aggro) {
                var other = target.GetComponent<Entity>();
                var color = closest != null && other == closest ? Color.green : Color.yellow;
                var a = entity.Position;
                var b = other.Position;
                a.y = a.y + 1f;
                DrawingTools.Line(a, b, color);
                // DrawingTools.Arrow(a, direction.normalized * (distance / 3), color);
            }
        }
    }

    public static void DrawDestination(Entity entity) {
        var origin = entity.Position;
        var destination = (Vector3) entity.Movable.Destination;

        DrawingTools.CircleWithLine(
                origin,
                destination,
                .3f,
                Selection.Instance.DestinationColor
        );
    }

    public static void DrawTarget(Entity entity) {
        var origin = entity.Position;
        var destination = entity.Interaction.Target.Position;
        origin.y += 1f;
        destination.y += 1f;
        DrawingTools.Line(origin, destination, Selection.Instance.TargetColor);
    }
}
