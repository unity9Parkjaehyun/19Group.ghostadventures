﻿using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;
    public Transform target;
    public float zoomInSize = 2f;
    public float zoomDuration = 1.5f;

    private float originalSize;
    private Vector3 originalCamPosition;  // 🔸 초기 카메라 위치 저장용
    private Tween zoomTween;
    private bool isZoomedIn = false;

    void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player").transform; // 플레이어 태그로 타겟 설정
        originalSize = virtualCam.m_Lens.OrthographicSize;
        originalCamPosition = virtualCam.transform.position; // 카메라 초기 위치 저장
        virtualCam.Follow = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ZoomIn();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ZoomOut();
        }
    }

    public void ZoomIn()
    {
        if (isZoomedIn) return;
        isZoomedIn = true;

        virtualCam.Follow = null; // Follow 제거 후 직접 위치 이동

        Vector3 targetPos = new Vector3(
            target.position.x,
            target.position.y,
            originalCamPosition.z 
        );

        zoomTween?.Kill();

        Sequence zoomInSequence = DOTween.Sequence();

        zoomInSequence.Append(
            DOTween.To(
                () => virtualCam.m_Lens.OrthographicSize,
                value => virtualCam.m_Lens.OrthographicSize = value,
                zoomInSize,
                zoomDuration
            ).SetEase(Ease.OutCubic)
        );

        zoomInSequence.Join(
            virtualCam.transform.DOMove(targetPos, zoomDuration).SetEase(Ease.OutCubic)
        );


    }

    public void ZoomOut()
    {
        if (!isZoomedIn) return;
        isZoomedIn = false;

        zoomTween?.Kill();

        // Follow 먼저 끊으면 위치 고정됨
        virtualCam.Follow = null;

        // 카메라 줌 + 위치 이동 동시에
        Sequence zoomOutSequence = DOTween.Sequence();

        zoomOutSequence.Append(
            DOTween.To(
                () => virtualCam.m_Lens.OrthographicSize,
                value => virtualCam.m_Lens.OrthographicSize = value,
                originalSize,
                zoomDuration
            ).SetEase(Ease.InOutCubic)
        );

        zoomOutSequence.Join(
            virtualCam.transform.DOMove(originalCamPosition, zoomDuration).SetEase(Ease.InOutCubic)
        );
    }

}
