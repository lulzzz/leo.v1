import template from './envelopes.html';

export const EnvelopesComponent = {
  template,
  bindings : {
    boardId : '<',
    envelopes : '<'
  },
  controller : class EnvelopesComponent{
  }
}