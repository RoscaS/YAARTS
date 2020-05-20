using UnityEngine;

public static class Ballistic
{
    private static readonly float gravity = Mathf.Abs(Physics.gravity.y);


    public static Vector3 ComputeInitialVelocity(Vector3 origin,
                                                 Vector3 target,
                                                 float speed,
                                                 bool useGravity) {
    
        var distance = target - origin;

        var time = distance.magnitude / speed;
        float HorizontalSpeed = distance.magnitude / time;
    
        Vector3 velocity = distance.normalized * HorizontalSpeed;
        velocity.y = useGravity ? distance.y / time + 0.5f * gravity * time : 0;

        return velocity;
    }

}
