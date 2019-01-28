﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PillarStates
{
    Intact,
    Cracked,
    Fractured,
    Wrecked,
    Destroyed,
    Count
}

public class Pillar : MonoBehaviour
{
    [HideInInspector]
    public PillarStates m_state;

    public SpriteRenderer m_spriteRenderer;
    public Sprite[] m_sprites;

    public bool IsDestructing { get { return (m_state > 0); } }

    public Vector2 pillarNumRow;

    public PillarManager pillarManager;

    private float m_destructDuration = 2f;
    private float m_destructTime;

    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        ResetDestructTime();
    }

    void Update()
    {
        if (IsDestructing && m_state != PillarStates.Destroyed)
        {
            m_destructTime -= Time.deltaTime;
            if (m_destructTime <= 0)
            {
                NextState();
                Debug.Log(m_state);
                m_destructTime = m_destructDuration;
            }
        }

        /*
        if (m_spriteRenderer != null)
            m_spriteRenderer.color = new Color(
                m_spriteRenderer.color.r,
                m_spriteRenderer.color.g,
                m_spriteRenderer.color.b,
                1 - ((float)m_state/((float)PillarStates.Count-1)));
        */
        //Debug.Log(((1/5)));

        if(m_state == PillarStates.Destroyed)
        {
            pillarManager.ReportPillarDestruction((int)pillarNumRow.y);
        }
    }

    public void TriggerDestruction()
    {
        NextState();
    }

    private void NextState()
    {
        m_state = m_state + 1;
        if (m_state != PillarStates.Destroyed)
            m_spriteRenderer.sprite = m_sprites[(int)m_state];
        else
            m_spriteRenderer.color = Color.clear;
    }

    public void Repair()
    {
        m_state = PillarStates.Intact;
        m_spriteRenderer.sprite = m_sprites[0];
        m_spriteRenderer.color = Color.white;
    }

    public void ResetDestructTime()
    {
        m_destructTime = m_destructDuration;
    }
}