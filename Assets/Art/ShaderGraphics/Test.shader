Shader "Unlit/Test"
{
    // �ν����Ϳ��� ������ �� �ִ� ������ �������ϴ�.
    Properties{
        _Scale ("Pattern Size", Range(0,10)) = 1
        _EvenColor("Color 1", Color) = (0,0,0,1)
        _OddColor("Color 2", Color) = (1,1,1,1)
    }

    SubShader{
        // ���׸����� ������ �������ϰ�, �ٸ� ������ ������Ʈ���� ���� Ÿ�ֿ̹� �����˴ϴ�.
        Tags{ "RenderType"="Opaque" "Queue"="Geometry"}


        Pass{
            CGPROGRAM
            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag

            float _Scale;

            float4 _EvenColor;
            float4 _OddColor;

            struct appdata{
                float4 vertex : POSITION;
            };

            struct v2f{
                float4 position : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            v2f vert(appdata v){
                v2f o;
                // ������ �� �ֵ��� ���ؽ� ��ǥ�� ������Ʈ �������� Ŭ�� �������� ��ȯ
                o.position = UnityObjectToClipPos(v.vertex);
                //calculate the position of the vertex in the world
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_TARGET{
                // ���̴� �Է¿� �°� ������ ���� ũ�⸦ �����ϰ�, ���� ���� ��� ���� ���� ó���մϴ�.
                float3 adjustedWorldPos = floor(i.worldPos / _Scale);
                // �ٸ� ����(��) �߰�
                float chessboard = adjustedWorldPos.x + adjustedWorldPos.y + adjustedWorldPos.z;
                // ���� 2�� ���� �Ҽ� �κ��� �����ɴϴ�. ¦���� ��� 0, Ȧ���� ��� 0.5�� �˴ϴ�.
                chessboard = frac(chessboard * 0.5);
                // 2�� ���� Ȧ���� ��� ȸ��(0.5)�� �ƴ� �Ͼ��(1)���� ����ϴ�.
                chessboard *= 2;

                // ¦�� ����(0)�� ����� Ȧ�� ����(1)�� ���� ���̸� �����մϴ�.
                float4 color = lerp(_EvenColor, _OddColor, chessboard);
                return color;
            }

            ENDCG
        }
    }
    FallBack "Standard" // FallBack ���� �ٸ� ������Ʈ�� �׸��ڰ� ������ �׸��� �н��� �߰��մϴ�.
}