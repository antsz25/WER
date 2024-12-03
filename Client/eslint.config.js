import vuePlugin from 'eslint-plugin-vue'
import vueTsEslintConfig from '@vue/eslint-config-typescript'
import skipFormatting from '@vue/eslint-config-prettier/skip-formatting'
import { Linter } from 'eslint'

/** @type {Linter.Config[]} */
export default [
  {
    // Apply rules to specific file types
    files: ['**/*.{ts,mts,tsx,vue}'],
    ignores: ['**/dist/**', '**/dist-ssr/**', '**/coverage/**'],
    plugins: {
      vue: vuePlugin,
      '@typescript-eslint': vueTsEslintConfig,
    },
    rules: {
      // Vue rules
      ...vuePlugin.configs['flat/essential'].rules,
      'vue/multi-word-component-names': ['error', { ignores: ['default'] }],

      // TypeScript and Prettier rules
      ...vueTsEslintConfig.rules,
      ...skipFormatting.rules,
    },
  }
]
