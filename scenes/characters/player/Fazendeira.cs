using Godot;
using System;

namespace Fazendinha.Characters;

public partial class Fazendeira : CharacterBody2D
{
	// [Export] permite editar esse valor diretamente pelo Inspector na Godot!
	[Export] public float Speed { get; set; } = 120.0f;

	// Referência para o nó AnimatedSprite2D que gerencia as animações
	private AnimatedSprite2D _animSprite;

	public override void _Ready()
	{
		// Busca o nó filho chamado "fazendeira" configurado na cena
		_animSprite = GetNode<AnimatedSprite2D>("fazendeira");
	}

	public override void _PhysicsProcess(double delta)
	{
		// 1. Captura o vetor de movimentação (X e Y) normalizado
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

		// 2. Aplica a velocidade baseada na direção
		Velocity = direction * Speed;

		// 3. Move o personagem e trata colisões
		MoveAndSlide();

		// 4. Atualiza a animação correspondente
		UpdateAnimation(direction);
	}

	/// <summary>
	/// Escolhe e reproduz a animação com base no vetor de movimento.
	/// </summary>
	private void UpdateAnimation(Vector2 direction)
	{
		// Se não há movimento, volta para a animação de repouso (idle)
		if (direction == Vector2.Zero)
		{
			_animSprite.Play("idle");
			return;
		}

		// Se o movimento horizontal (X) for maior que o vertical (Y)
		if (Mathf.Abs(direction.X) > Mathf.Abs(direction.Y))
		{
			if (direction.X > 0)
			{
				_animSprite.Play("walk_right");
			}
			else
			{
				_animSprite.Play("walk_left");
			}
		}
		// Se o movimento vertical (Y) predominar
		else
		{
			if (direction.Y > 0)
			{
				_animSprite.Play("walk_down");
			}
			else
			{
				_animSprite.Play("walk_top");
			}
		}
	}
}
