Shader "Unlit/Test"
{
    // 인스펙터에서 조작할 수 있는 값들이 보여집니다.
    Properties{
        _Scale ("Pattern Size", Range(0,10)) = 1
        _EvenColor("Color 1", Color) = (0,0,0,1)
        _OddColor("Color 2", Color) = (1,1,1,1)
    }

    SubShader{
        // 메테리얼은 완전히 불투명하고, 다른 불투명 지오메트리와 같은 타이밍에 렌더됩니다.
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
                // 렌더될 수 있도록 버텍스 좌표를 오브젝트 공간에서 클립 공간으로 변환
                o.position = UnityObjectToClipPos(v.vertex);
                //calculate the position of the vertex in the world
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_TARGET{
                // 셰이더 입력에 맞게 포지션 값의 크기를 조절하고, 정수 값을 얻기 위해 내림 처리합니다.
                float3 adjustedWorldPos = floor(i.worldPos / _Scale);
                // 다른 차원(축) 추가
                float chessboard = adjustedWorldPos.x + adjustedWorldPos.y + adjustedWorldPos.z;
                // 값을 2로 나눠 소수 부분을 가져옵니다. 짝수인 경우 0, 홀수인 경우 0.5가 됩니다.
                chessboard = frac(chessboard * 0.5);
                // 2를 곱해 홀수인 경우 회색(0.5)이 아닌 하얀색(1)으로 만듭니다.
                chessboard *= 2;

                // 짝수 영역(0)의 색상과 홀수 영역(1)의 색상 사이를 보간합니다.
                float4 color = lerp(_EvenColor, _OddColor, chessboard);
                return color;
            }

            ENDCG
        }
    }
    FallBack "Standard" // FallBack 으로 다른 오브젝트에 그림자가 지도록 그림자 패스를 추가합니다.
}