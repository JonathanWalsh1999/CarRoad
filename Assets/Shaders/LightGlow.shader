Shader "Custom/LightGlow"
{
    Properties{
      _Color("Color", Color) = (1, 0.5, 0, 1) // Amber color
      _Emission("Emission", Range(0, 1)) = 1
      _Attenuation("Attenuation", Range(0, 1)) = 0.9
      _BlurAmount("Blur Amount", Range(0, 1)) = 0.1

    }

        SubShader{
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            LOD 100

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata_t {
                    float4 vertex : POSITION;
                };

                struct v2f {
                    float4 pos : SV_POSITION;
                };

                float4 _Color;
                float _Emission;
                float _Attenuation;
                float _BlurAmount;

                v2f vert(appdata_t v) {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    return o;
                }

                half4 frag(v2f i) : SV_Target{
                    half3 glowColor = _Color.rgb * _Emission;
                    float2 texCoord = i.pos.xy / i.pos.w; // Convert to screen space
                    float distance = length(texCoord);

                    //if (distance <= _Attenuation) {
                        float attenuation = 1 - _Attenuation / distance;
                        glowColor += _Color.rgb * _Emission * attenuation;

                        // Apply blur effect
                        //float blurFactor = smoothstep(_Attenuation, _Attenuation - _BlurAmount, distance);

                        //glowColor = lerp(glowColor, _Color.rgb * _Emission, blurFactor);
                    
                    //}

                    return half4(glowColor, 1);
                }
                ENDCG
            }
    }
}