import angular from 'angular';
import { BoardsModule } from './boards/boards.module';
import { BoardModule } from './board/board.module';
import { AuthModule } from './auth/auth.module';

export const ComponentsModule = angular
  .module('app.components', [
    BoardsModule,
    BoardModule,
    AuthModule
  ])
  .name;