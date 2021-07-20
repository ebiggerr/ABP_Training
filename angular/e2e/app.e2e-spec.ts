import { demoTemplatePage } from './app.po';

describe('demo App', function() {
  let page: demoTemplatePage;

  beforeEach(() => {
    page = new demoTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
